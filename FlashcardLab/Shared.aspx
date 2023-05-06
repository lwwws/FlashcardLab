<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Shared.aspx.cs" Inherits="FlashcardLab.Shared" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/shared.css" />


    <div class="jumbotron bfield">
        <h1 class="smoke txt-logo">Shared decks</h1>
        <h3 class="cyan">FlashcardLab provides you with a platform to share your decks with other users!</h3>
        <h4>Browse, rate, comment or upload your own!</h4>
    </div>

    <asp:Label runat="server" ID="alert" CssClass="sm-field center spaced"/>

    <div class="container spaced">
        <div class="row">
            <div class="col-md-2">
                <asp:DropDownList ID="langSearch" runat="server" CssClass="form-control input-md">
                        <asp:ListItem value="">Any language</asp:ListItem>
                        <asp:ListItem value="misc">Other</asp:ListItem>
                        <asp:ListItem value="af">Afrikaans</asp:ListItem>
                        <asp:ListItem value="sq">Albanian - shqip</asp:ListItem>
                        <asp:ListItem value="am">Amharic - አማርኛ</asp:ListItem>
                        <asp:ListItem value="ar">Arabic - العربية</asp:ListItem>
                        <asp:ListItem value="an">Aragonese - aragonés</asp:ListItem>
                        <asp:ListItem value="hy">Armenian - հայերեն</asp:ListItem>
                        <asp:ListItem value="ast">Asturian - asturianu</asp:ListItem>
                        <asp:ListItem value="az">Azerbaijani - azərbaycan dili</asp:ListItem>
                        <asp:ListItem value="eu">Basque - euskara</asp:ListItem>
                        <asp:ListItem value="be">Belarusian - беларуская</asp:ListItem>
                        <asp:ListItem value="bn">Bengali - বাংলা</asp:ListItem>
                        <asp:ListItem value="bs">Bosnian - bosanski</asp:ListItem>
                        <asp:ListItem value="br">Breton - brezhoneg</asp:ListItem>
                        <asp:ListItem value="bg">Bulgarian - български</asp:ListItem>
                        <asp:ListItem value="ca">Catalan - català</asp:ListItem>
                        <asp:ListItem value="ckb">Central Kurdish - کوردی (دەستنوسی عەرەبی)</asp:ListItem>
                        <asp:ListItem value="zh">Chinese - 中文</asp:ListItem>
                        <asp:ListItem value="zh-HK">Chinese (Hong Kong) - 中文（香港）</asp:ListItem>
                        <asp:ListItem value="zh-CN">Chinese (Simplified) - 中文（简体）</asp:ListItem>
                        <asp:ListItem value="zh-TW">Chinese (Traditional) - 中文（繁體）</asp:ListItem>
                        <asp:ListItem value="co">Corsican</asp:ListItem>
                        <asp:ListItem value="hr">Croatian - hrvatski</asp:ListItem>
                        <asp:ListItem value="cs">Czech - čeština</asp:ListItem>
                        <asp:ListItem value="da">Danish - dansk</asp:ListItem>
                        <asp:ListItem value="nl">Dutch - Nederlands</asp:ListItem>
                        <asp:ListItem value="en">English</asp:ListItem>
                        <asp:ListItem value="en-AU">English (Australia)</asp:ListItem>
                        <asp:ListItem value="en-CA">English (Canada)</asp:ListItem>
                        <asp:ListItem value="en-IN">English (India)</asp:ListItem>
                        <asp:ListItem value="en-NZ">English (New Zealand)</asp:ListItem>
                        <asp:ListItem value="en-ZA">English (South Africa)</asp:ListItem>
                        <asp:ListItem value="en-GB">English (United Kingdom)</asp:ListItem>
                        <asp:ListItem value="en-US">English (United States)</asp:ListItem>
                        <asp:ListItem value="eo">Esperanto - esperanto</asp:ListItem>
                        <asp:ListItem value="et">Estonian - eesti</asp:ListItem>
                        <asp:ListItem value="fo">Faroese - føroyskt</asp:ListItem>
                        <asp:ListItem value="fil">Filipino</asp:ListItem>
                        <asp:ListItem value="fi">Finnish - suomi</asp:ListItem>
                        <asp:ListItem value="fr">French - français</asp:ListItem>
                        <asp:ListItem value="fr-CA">French (Canada) - français (Canada)</asp:ListItem>
                        <asp:ListItem value="fr-FR">French (France) - français (France)</asp:ListItem>
                        <asp:ListItem value="fr-CH">French (Switzerland) - français (Suisse)</asp:ListItem>
                        <asp:ListItem value="gl">Galician - galego</asp:ListItem>
                        <asp:ListItem value="ka">Georgian - ქართული</asp:ListItem>
                        <asp:ListItem value="de">German - Deutsch</asp:ListItem>
                        <asp:ListItem value="de-AT">German (Austria) - Deutsch (Österreich)</asp:ListItem>
                        <asp:ListItem value="de-DE">German (Germany) - Deutsch (Deutschland)</asp:ListItem>
                        <asp:ListItem value="de-LI">German (Liechtenstein) - Deutsch (Liechtenstein)</asp:ListItem>
                        <asp:ListItem value="de-CH">German (Switzerland) - Deutsch (Schweiz)</asp:ListItem>
                        <asp:ListItem value="el">Greek - Ελληνικά</asp:ListItem>
                        <asp:ListItem value="gn">Guarani</asp:ListItem>
                        <asp:ListItem value="gu">Gujarati - ગુજરાતી</asp:ListItem>
                        <asp:ListItem value="ha">Hausa</asp:ListItem>
                        <asp:ListItem value="haw">Hawaiian - ʻŌlelo Hawaiʻi</asp:ListItem>
                        <asp:ListItem value="he">Hebrew - עברית</asp:ListItem>
                        <asp:ListItem value="hi">Hindi - हिन्दी</asp:ListItem>
                        <asp:ListItem value="hu">Hungarian - magyar</asp:ListItem>
                        <asp:ListItem value="is">Icelandic - íslenska</asp:ListItem>
                        <asp:ListItem value="id">Indonesian - Indonesia</asp:ListItem>
                        <asp:ListItem value="ia">Interlingua</asp:ListItem>
                        <asp:ListItem value="ga">Irish - Gaeilge</asp:ListItem>
                        <asp:ListItem value="it">Italian - italiano</asp:ListItem>
                        <asp:ListItem value="it-IT">Italian (Italy) - italiano (Italia)</asp:ListItem>
                        <asp:ListItem value="it-CH">Italian (Switzerland) - italiano (Svizzera)</asp:ListItem>
                        <asp:ListItem value="ja">Japanese - 日本語</asp:ListItem>
                        <asp:ListItem value="kn">Kannada - ಕನ್ನಡ</asp:ListItem>
                        <asp:ListItem value="kk">Kazakh - қазақ тілі</asp:ListItem>
                        <asp:ListItem value="km">Khmer - ខ្មែរ</asp:ListItem>
                        <asp:ListItem value="ko">Korean - 한국어</asp:ListItem>
                        <asp:ListItem value="ku">Kurdish - Kurdî</asp:ListItem>
                        <asp:ListItem value="ky">Kyrgyz - кыргызча</asp:ListItem>
                        <asp:ListItem value="lo">Lao - ລາວ</asp:ListItem>
                        <asp:ListItem value="la">Latin</asp:ListItem>
                        <asp:ListItem value="lv">Latvian - latviešu</asp:ListItem>
                        <asp:ListItem value="ln">Lingala - lingála</asp:ListItem>
                        <asp:ListItem value="lt">Lithuanian - lietuvių</asp:ListItem>
                        <asp:ListItem value="mk">Macedonian - македонски</asp:ListItem>
                        <asp:ListItem value="ms">Malay - Bahasa Melayu</asp:ListItem>
                        <asp:ListItem value="ml">Malayalam - മലയാളം</asp:ListItem>
                        <asp:ListItem value="mt">Maltese - Malti</asp:ListItem>
                        <asp:ListItem value="mr">Marathi - मराठी</asp:ListItem>
                        <asp:ListItem value="mn">Mongolian - монгол</asp:ListItem>
                        <asp:ListItem value="ne">Nepali - नेपाली</asp:ListItem>
                        <asp:ListItem value="no">Norwegian - norsk</asp:ListItem>
                        <asp:ListItem value="nb">Norwegian Bokmål - norsk bokmål</asp:ListItem>
                        <asp:ListItem value="nn">Norwegian Nynorsk - nynorsk</asp:ListItem>
                        <asp:ListItem value="oc">Occitan</asp:ListItem>
                        <asp:ListItem value="or">Oriya - ଓଡ଼ିଆ</asp:ListItem>
                        <asp:ListItem value="om">Oromo - Oromoo</asp:ListItem>
                        <asp:ListItem value="ps">Pashto - پښتو</asp:ListItem>
                        <asp:ListItem value="fa">Persian - فارسی</asp:ListItem>
                        <asp:ListItem value="pl">Polish - polski</asp:ListItem>
                        <asp:ListItem value="pt">Portuguese - português</asp:ListItem>
                        <asp:ListItem value="pt-BR">Portuguese (Brazil) - português (Brasil)</asp:ListItem>
                        <asp:ListItem value="pt-PT">Portuguese (Portugal) - português (Portugal)</asp:ListItem>
                        <asp:ListItem value="pa">Punjabi - ਪੰਜਾਬੀ</asp:ListItem>
                        <asp:ListItem value="qu">Quechua</asp:ListItem>
                        <asp:ListItem value="ro">Romanian - română</asp:ListItem>
                        <asp:ListItem value="mo">Romanian (Moldova) - română (Moldova)</asp:ListItem>
                        <asp:ListItem value="rm">Romansh - rumantsch</asp:ListItem>
                        <asp:ListItem value="ru">Russian - русский</asp:ListItem>
                        <asp:ListItem value="gd">Scottish Gaelic</asp:ListItem>
                        <asp:ListItem value="sr">Serbian - српски</asp:ListItem>
                        <asp:ListItem value="sh">Serbo-Croatian - Srpskohrvatski</asp:ListItem>
                        <asp:ListItem value="sn">Shona - chiShona</asp:ListItem>
                        <asp:ListItem value="sd">Sindhi</asp:ListItem>
                        <asp:ListItem value="si">Sinhala - සිංහල</asp:ListItem>
                        <asp:ListItem value="sk">Slovak - slovenčina</asp:ListItem>
                        <asp:ListItem value="sl">Slovenian - slovenščina</asp:ListItem>
                        <asp:ListItem value="so">Somali - Soomaali</asp:ListItem>
                        <asp:ListItem value="st">Southern Sotho</asp:ListItem>
                        <asp:ListItem value="es">Spanish - español</asp:ListItem>
                        <asp:ListItem value="es-AR">Spanish (Argentina) - español (Argentina)</asp:ListItem>
                        <asp:ListItem value="es-419">Spanish (Latin America) - español (Latinoamérica)</asp:ListItem>
                        <asp:ListItem value="es-MX">Spanish (Mexico) - español (México)</asp:ListItem>
                        <asp:ListItem value="es-ES">Spanish (Spain) - español (España)</asp:ListItem>
                        <asp:ListItem value="es-US">Spanish (United States) - español (Estados Unidos)</asp:ListItem>
                        <asp:ListItem value="su">Sundanese</asp:ListItem>
                        <asp:ListItem value="sw">Swahili - Kiswahili</asp:ListItem>
                        <asp:ListItem value="sv">Swedish - svenska</asp:ListItem>
                        <asp:ListItem value="tg">Tajik - тоҷикӣ</asp:ListItem>
                        <asp:ListItem value="ta">Tamil - தமிழ்</asp:ListItem>
                        <asp:ListItem value="tt">Tatar</asp:ListItem>
                        <asp:ListItem value="te">Telugu - తెలుగు</asp:ListItem>
                        <asp:ListItem value="th">Thai - ไทย</asp:ListItem>
                        <asp:ListItem value="ti">Tigrinya - ትግርኛ</asp:ListItem>
                        <asp:ListItem value="to">Tongan - lea fakatonga</asp:ListItem>
                        <asp:ListItem value="tr">Turkish - Türkçe</asp:ListItem>
                        <asp:ListItem value="tk">Turkmen</asp:ListItem>
                        <asp:ListItem value="tw">Twi</asp:ListItem>
                        <asp:ListItem value="uk">Ukrainian - українська</asp:ListItem>
                        <asp:ListItem value="ur">Urdu - اردو</asp:ListItem>
                        <asp:ListItem value="ug">Uyghur</asp:ListItem>
                        <asp:ListItem value="uz">Uzbek - o‘zbek</asp:ListItem>
                        <asp:ListItem value="vi">Vietnamese - Tiếng Việt</asp:ListItem>
                        <asp:ListItem value="wa">Walloon - wa</asp:ListItem>
                        <asp:ListItem value="cy">Welsh - Cymraeg</asp:ListItem>
                        <asp:ListItem value="fy">Western Frisian</asp:ListItem>
                        <asp:ListItem value="xh">Xhosa</asp:ListItem>
                        <asp:ListItem value="yi">Yiddish</asp:ListItem>
                        <asp:ListItem value="yo">Yoruba - Èdè Yorùbá</asp:ListItem>
                        <asp:ListItem value="zu">Zulu - isiZulu</asp:ListItem>
                    </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="modeSearch" runat="server" CssClass="form-control input-md">
                    <asp:ListItem Value="ASC">From oldest to newest</asp:ListItem>
                    <asp:ListItem Value="DESC">From newest to oldest</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-5">
                <asp:TextBox ID="querySearch" autocomplete="off" runat="server" CssClass="form-control input-md"></asp:TextBox>
            </div>
            <div class="col-md-2 align-items-start">
                <asp:Button Text="Search" ID="search" CssClass="btn btn-primary" ToolTip="Search decks" OnClick="search_Click" runat="server"></asp:Button>
            </div>
        </div>
    </div>

    <asp:GridView ID="sharedGrid" CssClass="table table-dark" GridLines="None" BorderStyle="None" HorizontalAlign="Center" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#212121" ShowFooter="false" ShowHeader="false" DataKeyNames="id" AllowPaging="true" runat="server" OnRowCommand="sharedGrid_RowCommand" OnPageIndexChanging="sharedGrid_PageIndexChanging" OnRowDataBound="sharedGrid_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-10">
                                <asp:Image CssClass="img-fluid img-thumbnail small-square mspaced" ImageUrl='<%# GetImageUrl((int)Eval("userID")) %>' runat="server" />
                                <asp:LinkButton CssClass="deckTitle mspaced text-wrap" Text='<%# Eval("name") %>' runat="server" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" ToolTip="Comments" CommandName="Comments" />

                                <br />
                                <asp:Label CssClass="deckSub mspaced text-wrap" Text='<%# Eval("description") %>' runat="server" />
                                <br />
                                <br />

                                <asp:Label CssClass="deckSub cyan" Text='<%# "Language: " + Eval("language") %>' runat="server" />

                                <asp:Label CssClass="deckSub cyan" Text='<%# "Created: " + Eval("creation").ToString().Substring(0, 10)%>' runat="server" />
                                <br />

                                <asp:Label CssClass="deckSub cyan" Text='<%# "Made by: " + Eval("username") %>' runat="server" />

                                <asp:Label CssClass="smoke" ID="deckIDlbl" Text='<%# " − ID of this deck: " + Eval("id") %>' runat="server" />

                                <br />
                                <br />
                                <ajaxToolkit:Rating ID="rate" ReadOnly="true" runat="server" StarCssClass="glyphicon glyphicon-star yellow increase no-click" WaitingStarCssClass="glyphicon glyphicon-star-empty yellow" EmptyStarCssClass="glyphicon glyphicon-star-empty yellow" FilledStarCssClass="glyphicon glyphicon-star yellow" CurrentRating='<%# Math.Round((double)Eval("rating")) %>'></ajaxToolkit:Rating>
                                <br />
                                <asp:Label ID="ratingStatus" CssClass="deckSub" runat="server" Text='<%# Math.Round((double)Eval("rating"), 2) + " (Total votes: " + Eval("total") + ")"%>'></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton CssClass="btn btn-success increase spaced-btn" ToolTip="Add" CommandName="Add" runat="server" OnClientClick="return confirm('Are you sure you want to add this deck?')" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><span class="glyphicon glyphicon-save-file increase"></span>&nbsp;Add</asp:LinkButton>
                                <asp:LinkButton ID="btnRemove" CssClass="btn btn-danger increase spaced-btn" ToolTip="Remove" CommandName="Remove" runat="server" OnClientClick="return confirm('Are you sure you want to remove this deck from store?')" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><span class="glyphicon glyphicon-trash increase"></span>&nbsp;Remove</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

       </Columns>

   <PagerStyle ForeColor="#ffffff" CssClass="gridPager" HorizontalAlign="Center"  />
   </asp:GridView>
</asp:Content>
