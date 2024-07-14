<%@ Page Title="" Language="C#" MasterPageFile="~/Client-Side.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Delivery_Project.MainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <style>
    /* כללי */
    body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background: linear-gradient(135deg, #2E2E2E, #1A1A1A);
        color: #FFFFFF;
        margin: 0;
        padding: 0;
        direction: rtl;
        transition: background 0.5s ease;
    }

    .container1 {
        width: 90%;
        max-width: 1200px;
        margin: auto;
        overflow: hidden;
    }

    /* אנימציית כניסה */
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }

    /* כפתורים */
    .register {
        text-align: center;
        margin: 20px 0;
    }

    .btn {
        background: linear-gradient(45deg, #3498db, #2980b9);
        color: white;
        padding: 12px 25px;
        border: none;
        border-radius: 30px;
        cursor: pointer;
        font-size: 1.1em;
        font-weight: bold;
        transition: all 0.3s ease;
        box-shadow: 0 4px 15px rgba(52, 152, 219, 0.3);
        position: relative;
        overflow: hidden;
        margin: 10px;
        display: inline-block;
    }

    .btn:hover {
        transform: translateY(-3px);
        box-shadow: 0 6px 20px rgba(52, 152, 219, 0.4);
    }

    .btn::after {
        content: '';
        position: absolute;
        top: 50%;
        left: 50%;
        width: 5px;
        height: 5px;
        background: rgba(255, 255, 255, 0.5);
        opacity: 0;
        border-radius: 100%;
        transform: scale(1, 1) translate(-50%);
        transform-origin: 50% 50%;
    }

    .btn:hover::after {
        animation: ripple 1s ease-out;
    }

    @keyframes ripple {
        0% {
            transform: scale(0, 0);
            opacity: 0.5;
        }
        100% {
            transform: scale(20, 20);
            opacity: 0;
        }
    }

    /* תיבות תוכן */
    .box {
        background: rgba(30, 30, 30, 0.8);
        color: #FFFFFF;
        padding: 30px;
        border-radius: 15px;
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
        margin-top: 30px;
        transition: all 0.3s ease;
        animation: fadeInUp 0.8s ease-out;
    }

    .box:hover {
        transform: translateY(-5px);
        box-shadow: 0 15px 40px rgba(0, 0, 0, 0.3);
    }

    .box h1 {
        text-align: center;
        color: #3498db;
        font-size: 2.8em;
        margin-bottom: 30px;
        text-shadow: 2px 2px 4px rgba(0,0,0,0.3);
    }

    .box h3 {
        color: #2980b9;
        font-size: 2em;
        margin-bottom: 20px;
        position: relative;
        display: inline-block;
    }

    .box h3::after {
        content: '';
        position: absolute;
        width: 50%;
        height: 2px;
        background-color: #2980b9;
        bottom: -5px;
        left: 0;
        transition: width 0.3s ease;
    }

    .box:hover h3::after {
        width: 100%;
    }

    .box p {
        text-align: justify;
        margin: 15px 0;
        font-size: 1.2em;
        line-height: 1.6;
    }

    /* Modal */
    .modal {
        display: none;
        position: fixed;
        z-index: 1000;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgba(0, 0, 0, 0.8);
        animation: fadeIn 0.3s ease-out;
    }

    .modal-content {
        background: linear-gradient(135deg, #2E2E2E, #1A1A1A);
        color: #FFFFFF;
        margin: 5% auto;
        padding: 30px;
        border: 1px solid #888;
        width: 90%;
        max-width: 500px;
        border-radius: 15px;
        box-shadow: 0 5px 30px rgba(0, 0, 0, 0.3);
        animation: slideIn 0.5s ease-out;
    }

    @keyframes fadeIn {
        from {opacity: 0;}
        to {opacity: 1;}
    }

    @keyframes slideIn {
        from {transform: translateY(-50px);}
        to {transform: translateY(0);}
    }

    .close {
        color: #CCCCCC;
        float: right;
        font-size: 28px;
        font-weight: bold;
        transition: all 0.3s ease;
    }

    .close:hover,
    .close:focus {
        color: #3498db;
        text-decoration: none;
        cursor: pointer;
        transform: rotate(90deg);
    }

    /* שדות קלט */
    .modal-content input[type="text"],
    .modal-content input[type="password"],
    .modal-content input[type="email"],
    .modal-content input[type="tel"],
    .modal-content input[type="number"],
    .modal-content select {
        width: 100%;
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: none;
        border-radius: 25px;
        box-sizing: border-box;
        background-color: rgba(255, 255, 255, 0.1);
        color: #FFFFFF;
        transition: all 0.3s ease;
    }

    .modal-content input:focus,
    .modal-content select:focus {
        background-color: rgba(255, 255, 255, 0.2);
        box-shadow: 0 0 8px rgba(52, 152, 219, 0.6);
    }

    /* Select2 התאמה */
    .select2-container--default .select2-selection--single {
        background-color: rgba(255, 255, 255, 0.1);
        border: none;
        border-radius: 25px;
        height: 40px;
    }

    .select2-container--default .select2-selection--single .select2-selection__rendered {
        color: #FFFFFF;
        line-height: 40px;
    }

    .select2-container--default .select2-selection--single .select2-selection__arrow {
        height: 38px;
    }

    .select2-dropdown {
        background-color: #2E2E2E;
        border: none;
    }

    .select2-container--default .select2-results__option--highlighted[aria-selected] {
        background-color: #3498db;
    }

    .user-greeting{
        font-size:30px;
        color:black;
    }
    .register1{
         font-size:30px;
        color:black;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container1">
        <% if (Session["Manager"] != null) { %>
            <div class="register1">
                <p>מנהל יקר, ברוך הבא!</p>
            </div>
        <% } else if (Session["Driver"] != null) { %>
            <div class="register1">
                <p>נהג יקר, ברוך הבא!</p>
            </div>
        <% } else if (Session["Customer"] != null) { %>
            <div class="register1">
                <p>לקוח יקר, ברוך הבא!</p>
            </div>
        <% } else { %>
            <div class="register1">
                <p>לקוחות חדשים מקבלים יותר!!!</p>
                <p>להצטרפות</p>
                <asp:Button ID="Register" CssClass="btn" Text="להרשמה" OnClientClick="showRegisterModal(); return false;" runat="server" />
                <asp:Button ID="Login" CssClass="btn" Text="התחברות" OnClientClick="showLoginChoiceModal(); return false;" runat="server" />
            </div>
        <% } %>

        <div class="box">
            <h1>פתרונות תובלה מהפכניים בהישג ידך</h1>
            <p>התחבר אל CargoWay ותהנה ממערכת מתקדמת לניהול תובלה שמתאימה בדיוק לצרכים שלך.</p>
            <h3>ברוכים הבאים ל-CargoWay</h3>
            <p>ב-CargoWay, אנחנו מבינים שלנהל צי של נהגים זו משימה מאתגרת ומורכבת. לכן, יצרנו פתרון חכם ויעיל שמקל עליך לנהל ולתכנן את העבודה של הנהגים בצורה המתקדמת ביותר.</p>
            <h3>מה אנחנו מציעים?</h3>
            <h3>ניהול מסלולים קל ופשוט:</h3>
            <p>האפליקציה שלנו מאפשרת לך ליצור, לתכנן ולנהל מסלולים במהירות ובפשטות. תוכל להקצות משימות לנהגים בצורה אופטימלית ולהבטיח שכל משלוח יגיע ליעדו בזמן.</p>
            <h3>מעקב בזמן אמת:</h3>
            <p>המערכת שלנו מאפשרת לך לעקוב אחרי המשלוחים בזמן אמת. תוכל לראות את מיקומם של הנהגים שלך בכל רגע נתון ולקבל עדכונים שוטפים לגבי מצב המשלוחים. כמו כן, תוכל לקבל התראות על עיכובים או בעיות במהלך המסלול, כדי שתוכל לפעול במהירות ולפתור בעיות בזמן אמת.</p>
            <h3>דוחות וניתוחים מתקדמים:</h3>
            <p>CargoWay מספקת לך כלים מתקדמים לניתוח ביצועים. תוכל להפיק דוחות מפורטים על ביצועי הנהגים, זמני המסירה, ודוחות כספיים שיעזרו לך לנהל את התקציב בצורה יעילה. כלים אלה יעזרו לך לקבל החלטות מושכלות ולשפר את יעילות העבודה.</p>
            <h3>ניהול לקוחות והזמנות:</h3>
            <p>המערכת שלנו מאפשרת לך לנהל את פרטי הלקוחות וההזמנות בצורה פשוטה ויעילה. תוכל לעקוב אחרי היסטוריית ההזמנות של כל לקוח ולשלוח התראות ועדכונים בזמן אמת. כמו כן, תוכל ליצור הצעות מחיר ולשלוח חשבוניות בצורה דיגיטלית, מה שמייעל את תהליך ניהול הלקוחות.</p>
            <h3>אינטגרציה עם מערכות אחרות:</h3>
            <p>CargoWay תומכת באינטגרציה מלאה עם מערכות ERP, CRM, ותוכנות ניהול מלאי אחרות. כך תוכל לסנכרן את כל הנתונים בצורה חלקה ולקבל תמונה מלאה ומדויקת של פעילות העסק שלך.</p>
            <h3>אבטחת מידע:</h3>
            <p>אבטחת המידע שלך חשובה לנו מאוד. מערכת CargoWay משתמשת בטכנולוגיות ההצפנה המתקדמות ביותר כדי להבטיח שהמידע שלך מוגן מפני גישה לא מורשית.</p>
            <h3>לנהגים</h3>
            <h3>מעקב אחרי סידור קווים:</h3>
            <p>ב-CargoWay, אנו מקלים על הנהגים עם גישה נוחה לרשימות סידור הקווים שלהם. באמצעות האפליקציה, הנהגים יכולים לבדוק את לוח הזמנים שלהם, לראות את המסלולים המוקצים להם, ולקבל הנחיות ברורות לכל משלוח.</p>
            <h3>ניהול זמן ומשימות:</h3>
            <p>הנהגים יכולים לנהל את הזמן שלהם בצורה יעילה באמצעות האפליקציה שלנו, לקבל התראות על משימות חדשות, ולסמן משימות כבוצעות בזמן אמת. זה עוזר לשפר את הדיוק והאמינות של המשלוחים.</p>
            <h3>תקשורת ישירה:</h3>
            <p>האפליקציה מאפשרת תקשורת ישירה בין הנהגים למנהלים, כך שתוכל לקבל הנחיות ועדכונים בזמן אמת, ולדווח על בעיות או עיכובים באופן מיידי.</p>
            <h3>ללקוחות</h3>
            <h3>שקיפות מלאה:</h3>
            <p>לקוחות CargoWay נהנים משקיפות מלאה בנוגע למצב המשלוחים שלהם. תוכל לעקוב אחרי המשלוח שלך בזמן אמת, לקבל עדכונים על הסטטוס, ולדעת בדיוק מתי הוא יגיע ליעדו.</p>
            <h3>ניהול חשבוניות ודוחות:</h3>
            <p>לקוחות יכולים לנהל את החשבוניות והדוחות שלהם בצורה דיגיטלית ולקבל גישה מהירה לכל המידע הפיננסי הקשור למשלוחים שלהם. זה מאפשר לך לעקוב אחרי ההוצאות והכנסות בצורה יעילה יותר.</p>
            <h3>למנהלים</h3>
            <h3>אופטימיזציה של משאבים:</h3>
            <p>מנהלי צי יכולים לנצל את CargoWay כדי לאתר ולייעל את השימוש במשאבים. תוכל לזהות נהגים וציוד שנמצאים במצב אופטימלי ולבצע התאמות במידת הצורך כדי לשפר את היעילות התפעולית.</p>
            <h3>מעקב אחר ביצועים:</h3>
            <p>מנהלים יכולים לעקוב אחרי ביצועי הנהגים והמשלוחים בעזרת דוחות מפורטים וגרפים ויזואליים. זה מאפשר לך לקבל תמונה ברורה של הפעילות העסקית ולבצע שיפורים מבוססי נתונים.</p>
            <h3>תכנון עתידי:</h3>
            <p>המערכת שלנו תומכת בתכנון עתידי בעזרת כלים לחיזוי וניתוח נתונים. תוכל לתכנן את הפעילות העסקית שלך קדימה ולהיות מוכן לכל תרחיש.</p>
            <h3>ניהול קווים ועדכונם:</h3>
            <p>המנהלים יכולים לנהל ולעדכן קווי נסיעה בקלות באמצעות המערכת שלנו. תוכל להוסיף ולערוך מסלולים בצורה גמישה ומהירה, ולהבטיח שהנהגים והלקוחות מקבלים את השירות הטוב ביותר.</p>
            <h3>קליטת נהגים וניהול שוטף:</h3>
            <p>המערכת מאפשרת למנהלים לקלוט נהגים חדשים ולנהל את הצוות בצורה שוטפת ויעילה. תוכל לנהל את פרטי הנהגים, להקצות להם משימות ולעקוב אחרי ביצועיהם בצורה חכמה ומקיפה.</p>
        </div>
    </div>

    <!-- תיבת הרשמה (Modal) -->
    <div id="registerModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="hideRegisterModal()">&times;</span>
            <h2>הרשמה</h2>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="שם מלא"></asp:TextBox><br />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="אימייל"></asp:TextBox><br />
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="סיסמה"></asp:TextBox><br />
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="טלפון"></asp:TextBox><br />
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="כתובת"></asp:TextBox><br />
           <div class="form-group">
                <label for="ddlCity">עיר:</label>
                <asp:DropDownList ID="DDLcity" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" Placeholder="חברה"></asp:TextBox><br />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="הרשם" OnClick="btnSubmit_Click" />
        </div>
    </div>

    <!-- תיבת בחירת סוג משתמש (Modal) -->
    <div id="loginChoiceModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="hideLoginChoiceModal()">&times;</span>
            <h2>בחר סוג משתמש</h2>
            <asp:Button ID="btnManagerLogin" runat="server" CssClass="btn btn-primary" Text="מנהל" OnClientClick="showManagerLoginModal(); return false;" />
            <asp:Button ID="btnDriverLogin" runat="server" CssClass="btn btn-secondary" Text="נהג" OnClientClick="showDriverLoginModal(); return false;" />
            <asp:Button ID="btnCustomerLogin" runat="server" CssClass="btn btn-success" Text="לקוח" OnClientClick="showCustomerLoginModal(); return false;" />
        </div>
    </div>

    <!-- תיבת התחברות מנהל (Modal) -->
    <div id="managerLoginModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="hideManagerLoginModal()">&times;</span>
            <h2>התחברות מנהל</h2>
            <asp:TextBox ID="txtManagerEmail" runat="server" CssClass="form-control" Placeholder="אימייל"></asp:TextBox><br />
            <asp:TextBox ID="txtManagerPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="סיסמה"></asp:TextBox><br />
            <asp:Button ID="btnManagerSubmit" runat="server" CssClass="btn btn-primary" Text="התחבר" OnClick="btnManagerLogin_Click" />
            <asp:Button ID="btnBackToChoiceFromManager" runat="server" CssClass="btn btn-secondary" Text="חזרה" OnClientClick="showLoginChoiceModalFromManager(); return false;" />
        </div>
    </div>

    <!-- תיבת התחברות נהג (Modal) -->
    <div id="driverLoginModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="hideDriverLoginModal()">&times;</span>
            <h2>התחברות נהג</h2>
            <asp:TextBox ID="txtDriverEmail" runat="server" CssClass="form-control" Placeholder="אימייל"></asp:TextBox><br />
            <asp:TextBox ID="txtDriverPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="סיסמה"></asp:TextBox><br />
            <asp:Button ID="btnDriverSubmit" runat="server" CssClass="btn btn-primary" Text="התחבר" OnClick="btnDriverLogin_Click" />
            <asp:Button ID="btnBackToChoiceFromDriver" runat="server" CssClass="btn btn-secondary" Text="חזרה" OnClientClick="showLoginChoiceModalFromDriver(); return false;" />
            <asp:Literal ID="LtlMsg" runat="server" ></asp:Literal>
        </div>
    </div>

    <!-- תיבת התחברות לקוח (Modal) -->
    <div id="customerLoginModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="hideCustomerLoginModal()">&times;</span>
            <h2>התחברות לקוח</h2>
            <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="form-control" Placeholder="אימייל"></asp:TextBox><br />
            <asp:TextBox ID="txtCustomerPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="סיסמה"></asp:TextBox><br />
            <asp:Button ID="btnCustomerSubmit" runat="server" CssClass="btn btn-primary" Text="התחבר" OnClick="btnCustomerLogin_Click" /><br />
            <asp:Button ID="btnBackToChoiceFromCustomer" runat="server" CssClass="btn btn-secondary" Text="חזרה" OnClientClick="showLoginChoiceModalFromCustomer(); return false;" /><br />
            <asp:Literal ID="LtlMsgCustomer" runat="server" EnableViewState="false"></asp:Literal>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script type="text/javascript">

        function showRegisterModal() {
            document.getElementById("registerModal").style.display = "block";
        }

        function hideRegisterModal() {
            document.getElementById("registerModal").style.display = "none";
        }

        function showLoginChoiceModal() {
            document.getElementById("loginChoiceModal").style.display = "block";
        }

        function hideLoginChoiceModal() {
            document.getElementById("loginChoiceModal").style.display = "none";
        }

        function showManagerLoginModal() {
            hideLoginChoiceModal();
            document.getElementById("managerLoginModal").style.display = "block";
        }

        function hideManagerLoginModal() {
            document.getElementById("managerLoginModal").style.display = "none";
        }

        function showDriverLoginModal() {
            hideLoginChoiceModal();
            document.getElementById("driverLoginModal").style.display = "block";
        }

        function hideDriverLoginModal() {
            document.getElementById("driverLoginModal").style.display = "none";
        }
        function showCustomerLoginModal() {
            hideLoginChoiceModal();
            document.getElementById("customerLoginModal").style.display = "block";
        }

        function hideCustomerLoginModal() {
            document.getElementById("customerLoginModal").style.display = "none";
        }

        function showLoginChoiceModalFromCustomer() {
            hideCustomerLoginModal();
            showLoginChoiceModal();
        }
        function showLoginChoiceModalFromManager() {
            hideManagerLoginModal();
            showLoginChoiceModal();
        }

        function showLoginChoiceModalFromDriver() {
            hideDriverLoginModal();
            showLoginChoiceModal();
        }
        function showErrorModal() {
            document.getElementById("driverLoginModal").style.display = "block";
        }

        // סגירת ה-modal בלחיצה מחוץ לתיבה
        window.onclick = function (event) {
            var registerModal = document.getElementById("registerModal");
            var loginChoiceModal = document.getElementById("loginChoiceModal");
            var managerLoginModal = document.getElementById("managerLoginModal");
            var driverLoginModal = document.getElementById("driverLoginModal");
            var customerLoginModal = document.getElementById("customerLoginModal");
            if (event.target == registerModal) {
                registerModal.style.display = "none";
            }
            if (event.target == loginChoiceModal) {
                loginChoiceModal.style.display = "none";
            }
            if (event.target == managerLoginModal) {
                managerLoginModal.style.display = "none";
            }
            if (event.target == driverLoginModal) {
                driverLoginModal.style.display = "none";
            }
            if (event.target == customerLoginModal) {
                customerLoginModal.style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= DDLcity.ClientID %>').select2({
                placeholder: 'נא הזן שם עיר',
            allowClear: true,
            width: '100%' // מתאים את רוחב הרכיב לרוחב השדה
        });
    });
</script>
</asp:Content>
