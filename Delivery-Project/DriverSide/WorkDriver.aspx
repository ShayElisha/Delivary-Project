<%@ Page Title="" Language="C#" MasterPageFile="~/DriverSide/DriverSide.Master" AutoEventWireup="true" CodeBehind="WorkDriver.aspx.cs" Inherits="Delivery_Project.DriverSide.WorkDriver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?key=<%= ConfigurationManager.AppSettings["SecretKey"] %>&libraries=places"></script>
    <script src="https://unpkg.com/axios/dist/axios.min.js"></script>
    <style>
        /* עיצוב כפתורים כלליים */
        .btn {
            background-color: #003366;
            color: #fff;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            font-size: 1em;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #0052cc;
        }

        /* עיצוב כפתורים בתוך טבלה */
        .table .btn {
            margin: 0 5px;
        }

        /* כפתורים מוסתרים */
        .hidden-btns {
            display: none;
        }

        /* עיצוב כפתור 'הוסף למשאית' כאשר הכפתורים המוסתרים מוצגים */
        .hidden-btns .btn {
            display: inline-block;
            margin: 5px 0;
        }

        body {
            font-family: 'Arial', sans-serif;
            background-color: #f0f0f5;
            color: #333;
        }

        .header {
            background-color: #003366;
            color: #fff;
            padding: 20px;
            text-align: center;
            font-size: 2em;
        }

        .content {
            padding: 20px;
        }

        .timer {
            position: absolute;
            top: 10px;
            left: 10px;
            font-size: 1.5em;
            background-color: #fff;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            display: none; /* Initially hidden */
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin-top: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .btn {
            background-color: #003366;
            color: #fff;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            font-size: 1em;
        }

        .btn:hover {
            background-color: #0052cc;
        }

        #map {
            height: 400px;
            width: 100%;
            margin-top: 20px;
            display: none;
        }

        .address-list {
            list-style-type: none;
            padding: 0;
        }

        .address-list li {
            background-color: #e6e6ff;
            margin: 10px 0;
            padding: 10px;
            border-radius: 5px;
        }

        .address-list li .address-title {
            font-weight: bold;
        }

        .address-list li .address-details {
            margin-top: 5px;
        }

        .table-container {
            display: none; /* Initially hidden */
        }

        .table {
            width: 100%;
            border-collapse: collapse;
        }

        .table th, .table td {
            padding: 10px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .error-message {
            color: red;
            display: none;
            margin-top: 10px;
        }

        .hidden-btns {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var timer;
        var elapsedSeconds = 0;
        var map;
        var markers = [];
        var isWorking = false;

        function startTimer() {
            document.getElementById("timer").style.display = 'block';
            document.getElementById("timer").style.zIndex = 10000;
            timer = setInterval(function () {
                elapsedSeconds++;
                localStorage.setItem('elapsedSeconds', elapsedSeconds);
                document.getElementById("timerDisplay").innerText = formatTime(elapsedSeconds);
            }, 1000);
        }

        function stopTimer() {
            document.getElementById("timer").style.display = 'none';
            clearInterval(timer);
            elapsedSeconds = 0;
            document.getElementById("timerDisplay").innerText = formatTime(elapsedSeconds);
            localStorage.removeItem('elapsedSeconds');
            localStorage.removeItem('isWorking');
        }

        function formatTime(seconds) {
            var hrs = Math.floor(seconds / 3600);
            var mins = Math.floor((seconds % 3600) / 60);
            var secs = seconds % 60;
            return pad(hrs) + ":" + pad(mins) + ":" + pad(secs);
        }

        function pad(value) {
            return value < 10 ? "0" + value : value;
        }

        function toggleWork() {
            var button = document.getElementById("workButton");
            var tableContainer = document.getElementById("tableContainer");
            var map = document.getElementById("map");
            if (!isWorking) {
                button.innerText = "סיים עבודה";
                startTimer();
                tableContainer.style.display = 'block';
                map.style.display = 'block';
                loadShipments();
                isWorking = true;
                localStorage.setItem('isWorking', true);
            } else {
                button.innerText = "התחל עבודה";
                stopTimer();
                alert('העבודה הסתיימה');
                tableContainer.style.display = 'none';
                map.style.display = 'none';
                isWorking = false;
            }
        }

        function isCityNameValid(city) {
            return !/\d/.test(city);
        }

        function getCoordinates(city, address) {
            return new Promise((resolve, reject) => {
                if (!isCityNameValid(city)) {
                    console.log(`Ignoring city with numbers: ${city}`);
                    return reject('City name contains numbers');
                }
                console.log(`Fetching coordinates for: ${address}, ${city}`);
                var url = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(address + ', ' + city)}&key=<%= ConfigurationManager.AppSettings["SecretKey"] %>`;
                axios.get(url)
                    .then(response => {
                        if (response.data.results.length > 0) {
                            console.log('Coordinates found:', response.data.results[0].geometry.location.lat, response.data.results[0].geometry.location.lng);
                            resolve({ lat: response.data.results[0].geometry.location.lat, lon: response.data.results[0].geometry.location.lng });
                        } else {
                            console.error('No coordinates found for:', address, city);
                            reject('No coordinates found');
                        }
                    })
                    .catch(error => {
                        console.error('Error with geocoding request:', error);
                        reject(error);
                    });
            });
        }

        function clearMarkers() {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
        }

        function loadShipments() {
            fetch('WorkDriver.aspx/GetShipments', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    loadMap(data.d);
                })
                .catch(error => console.error('Error loading shipments:', error));
        }

        function loadMap(shipments) {
            var mapDiv = document.getElementById('map');
            mapDiv.style.display = 'block';

            if (!map) {
                map = new google.maps.Map(mapDiv, {
                    center: { lat: 32.0853, lng: 34.7818 },
                    zoom: 13,
                    mapTypeId: 'roadmap'
                });
            } else {
                map.setCenter({ lat: 32.0853, lng: 34.7818 });
                map.setZoom(13);
            }

            clearMarkers();

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var currentPosition = { lat: position.coords.latitude, lng: position.coords.longitude };
                    var driverMarker = new google.maps.Marker({
                        position: currentPosition,
                        map: map,
                        icon: 'https://maps.google.com/mapfiles/ms/icons/green-dot.png'
                    });
                    markers.push(driverMarker);

                    var waypoints = [];
                    var visitedAddresses = {};  // Object to track visited addresses

                    var promises = [];

                    shipments.forEach(function (shipment) {
                        var sourceAddressKey = shipment.SourceAdd + ', ' + shipment.SourceCity;
                        var destinationAddressKey = shipment.DestinationAdd + ', ' + shipment.DestinationCity;

                        if (!visitedAddresses[sourceAddressKey]) {
                            visitedAddresses[sourceAddressKey] = true;
                            promises.push(getCoordinates(shipment.SourceCity, shipment.SourceAdd).then(sourceCoordinates => {
                                waypoints.push({
                                    location: { lat: sourceCoordinates.lat, lng: sourceCoordinates.lon },
                                    stopover: true
                                });

                                if (!visitedAddresses[destinationAddressKey]) {
                                    visitedAddresses[destinationAddressKey] = true;
                                    return getCoordinates(shipment.DestinationCity, shipment.DestinationAdd).then(destinationCoordinates => {
                                        waypoints.push({
                                            location: { lat: destinationCoordinates.lat, lng: destinationCoordinates.lon },
                                            stopover: true
                                        });
                                    });
                                }
                            }).catch(error => {
                                console.error('Error getting coordinates for source address:', error);
                            }));
                        } else if (!visitedAddresses[destinationAddressKey]) {
                            visitedAddresses[destinationAddressKey] = true;
                            promises.push(getCoordinates(shipment.DestinationCity, shipment.DestinationAdd).then(destinationCoordinates => {
                                waypoints.push({
                                    location: { lat: destinationCoordinates.lat, lng: destinationCoordinates.lon },
                                    stopover: true
                                });
                            }).catch(error => {
                                console.error('Error getting coordinates for destination address:', error);
                            }));
                        }
                    });

                    Promise.all(promises).then(() => {
                        if (waypoints.length > 1) {
                            var directionsService = new google.maps.DirectionsService();
                            var directionsRenderer = new google.maps.DirectionsRenderer();
                            directionsRenderer.setMap(map);

                            var request = {
                                origin: currentPosition,
                                destination: waypoints[waypoints.length - 1].location,
                                waypoints: waypoints,
                                optimizeWaypoints: true,
                                travelMode: 'DRIVING'
                            };

                            directionsService.route(request, function (result, status) {
                                if (status == 'OK') {
                                    directionsRenderer.setDirections(result);
                                }
                            });
                        }
                    });
                }, function (error) {
                    console.error('Error occurred. Error code: ' + error.code);
                }, {
                    enableHighAccuracy: true,
                    timeout: 5000,
                    maximumAge: 0
                });
            }
        }


        function navigate(destination) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var currentPosition = `${position.coords.latitude},${position.coords.longitude}`;
                    var url = `https://www.google.com/maps/dir/?api=1&origin=${currentPosition}&destination=${encodeURIComponent(destination)}&travelmode=driving`;
                    window.open(url, '_blank');
                }, function (error) {
                    console.error('Error occurred. Error code: ' + error.code);
                }, {
                    enableHighAccuracy: true,
                    timeout: 5000,
                    maximumAge: 0
                });
            }
        }

        function HandleAddToTruck(button) {
            __doPostBack(button.name, button.getAttribute('CommandArgument'));

            setTimeout(function () {
                loadShipments();
            }, 1000);
        }

        function showHiddenButtons(button) {
            var hiddenButtons = button.parentNode.querySelectorAll('.hidden-btns');
            hiddenButtons.forEach(function (btn) {
                btn.style.display = 'inline-block';
            });
            button.style.display = 'none';

            var shipmentId = button.getAttribute('data-shipment-id');
            localStorage.setItem('buttonState_' + shipmentId, 'clicked');
        }

        function restoreButtonState() {
            var buttons = document.querySelectorAll('[data-shipment-id]');
            buttons.forEach(function (button) {
                var shipmentId = button.getAttribute('data-shipment-id');
                if (localStorage.getItem('buttonState_' + shipmentId) === 'clicked') {
                    showHiddenButtons(button);
                }
            });
        }

        window.onload = function () {
            if (localStorage.getItem('isWorking') === 'true') {
                elapsedSeconds = parseInt(localStorage.getItem('elapsedSeconds'), 10) || 0;
                document.getElementById("timer").style.display = 'block';
                document.getElementById("workButton").innerText = "סיים עבודה";
                isWorking = true;
                startTimer();
                loadShipments();
                document.getElementById('tableContainer').style.display = 'block';
                document.getElementById('map').style.display = 'block';
            } else {
                loadMap([]);
            }

            restoreButtonState();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        שלום נהג יקר
    </div>
    <div class="content">
        <div class="timer" id="timer">
            <div id="timerDisplay">00:00:00</div>
        </div>
        <div class="form-group">
            <button id="workButton" class="btn" onclick="toggleWork(); return false;">התחל עבודה</button>
        </div>
        <div class="table-container" id="tableContainer">
            <table class="table table-striped table-bordered table-hover" id="mainTbl">
                <thead>
                    <tr>
                        <th>מזהה משלוח</th>
                        <th>מזהה הזמנה</th>
                        <th>מזהה לקוח</th>
                        <th>כתובת מקור</th>
                        <th>עיר מקור</th>
                        <th>כתובת יעד</th>
                        <th>עיר יעד</th>
                        <th>טלפון</th>
                        <th>כמות</th>
                        <th>פעולות</th>
                    </tr>
                </thead>
                <tbody id="addressTableBody">
                    <asp:Repeater ID="RptProd" runat="server">
                        <ItemTemplate>
                            <tr class="odd gradeX">
                                <td><%#Eval("ShipId") %></td>
                                <td><%#Eval("OrderID") %></td>
                                <td><%#Eval("CustomerID") %></td>
                                <td><%#Eval("SourceAdd") %></td>
                                <td><%#Eval("SourceCity") %></td>
                                <td><%#Eval("DestinationAdd") %></td>
                                <td><%#Eval("DestinationCity") %></td>
                                <td><%#Eval("Phone") %></td>
                                <td><%#Eval("Quantity") %></td>
                                <td>
                                    <asp:Button ID="AddToo" runat="server" CssClass="btn" Text="הוסף למשאית" OnClientClick="HandleAddToTruck(this); return false;" CommandArgument='<%# Eval("ShipId") + "," + Eval("Phone") + "," + Eval("OrderID") %>' OnClick="AddToTruckButton_Click" data-shipment-id='<%# Eval("ShipId") %>' />
                                    <div class="hidden-btns">
                                        <asp:Button ID="btnNavigate" runat="server" CssClass="btn" Text="Navigate" OnClientClick='<%# "navigate(\"" + Eval("DestinationAdd") + ", " + Eval("DestinationCity") + "\"); return false;" %>' />
                                        <asp:Button ID="Button2" runat="server" CssClass="btn" Text="נמסר" CommandArgument='<%# Eval("ShipId") + "," + Eval("Phone") + "," + Eval("OrderID") %>' OnClick="DeliverOrderButton_Click" />
                                        <asp:Button ID="Button3" runat="server" CssClass="btn" Text="שלח הודעת וואטסאפ" CommandArgument='<%# Eval("Phone") %>' OnClick="SendWhatsAppButton_Click" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div id="map"></div>
        <div class="error-message" id="error-message"></div>
    </div>
</asp:Content>
