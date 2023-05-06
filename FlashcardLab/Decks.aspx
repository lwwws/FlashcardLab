<%@ Page Title="Decks" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Decks.aspx.cs" Inherits="FlashcardLab.Decks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/decks.css" />
    <div class="jumbotron bfield">
        <h1 class="smoke txt-logo">My decks</h1>
    </div>

    <div class="alert alert-danger alert-dismissible" role="alert" id="listAlert" runat="server">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
    </div>

    <asp:ListView ID="decksList" runat="server" DataKeyNames="id" GroupItemCount="3" InsertItemPosition="LastItem" OnItemDataBound="decksList_ItemDataBound" GroupPlaceholderID="ph1" ItemPlaceholderID="ph2" OnSelectedIndexChanging="decksList_SelectedIndexChanging" OnItemEditing="decksList_ItemEditing" OnItemCanceling="decksList_ItemCanceling" OnItemDeleting="decksList_ItemDeleting" OnItemInserting="decksList_ItemInserting" OnItemUpdating="decksList_ItemUpdating" OnItemCommand="decksList_ItemCommand" OnPagePropertiesChanging="decksList_PagePropertiesChanging">
        <LayoutTemplate>
            <div class="container" runat="server">
                <div class="row parentCenter sm-field mspaced pager">
                    <asp:DataPager runat="server" ID="ItemDataPager" PageSize="8" PagedControlID="decksList">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Link"
                                ShowFirstPageButton="true"
                                ShowPreviousPageButton="true"
                                ShowLastPageButton="true"
                                ShowNextPageButton ="true"/>

                            <asp:TemplatePagerField>
                                <PagerTemplate>
                                    <b>
                                    (<asp:Label runat="server" Text="<%# Container.TotalRowCount%>" />
                                    decks total)
                                    <br />
                                    </b>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                </div>
                <div class="row" runat="server" id="ph1" ></div>
            </div>
        </LayoutTemplate>
        <GroupSeparatorTemplate>
            <br />
        </GroupSeparatorTemplate>
        <GroupTemplate>
            <div class="row equal" runat="server" id="tableRow">
                <div class="col" runat="server" id="ph2" />
            </div>
        </GroupTemplate>
        <ItemTemplate>
            <div class="col-md-4">
                <div class="item-deck">
                <asp:LinkButton runat="server" ID="deckBtn" CommandName="Select" Font-Underline="false">
                <div class="panel panel-default dpanel" runat="server" ID="itemDeck">
                    <div class="panel-heading dhead" id="phead" runat="server">
                        <asp:Label Text='<%# Eval("name") %>' CssClass="smoke enhance-x" runat="server" />
                    </div>
                    <div class="panel-body dbody">
                        <asp:Label Text='<%# Eval("description") %>' runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="languageLbl" Text='<%# "Language:" + Eval("language") %>' runat="server" />
                        <br />
                    </div>
                    <div class="panel-footer dfooter">
                        <asp:Label Text='<%# "Next Review: " + Eval("next") %>' runat="server" />
                        <br />
                        <div class="progress">
                            <div class="progress-bar progress-bar-success black pad-sm" id="deckBar" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width:40%">
                            <%# Eval("progress") + "% Complete" %>
                            </div>
                        </div>
                    </div>
                </div>
                </asp:LinkButton>
                </div>
            </div>
        </ItemTemplate>
        <SelectedItemTemplate>
            <div class="col-md-4">
                <div class="item-deck">
                <div class="panel panel-default spanel" runat="server" ID="itemDeck">
                    <div class="panel-heading" id="phead" runat="server">
                        <asp:Label Text='<%# Eval("name") %>' CssClass="smoke enhance-x" runat="server" />
                    </div>
                    <div class="panel-body sbody">
                        <asp:Label Text='<%# Eval("description") %>' runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="languageLbl" Text='<%# "Language: " + Eval("language") %>' runat="server" />
                        <br />
                    </div>
                    <div class="panel-footer sfooter slide">
                        <asp:Label Text='<%# "Next Review: " + Eval("next") %>' runat="server" />
                        <br />
                        <div class="progress hide">
                            <div class="progress-bar progress-bar-success black pad-sm" id="deckBar" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width:40%">
                            <%# Eval("progress") + "% Complete" %>
                            </div>
                        </div>
                        <asp:LinkButton ID="review" runat="server" CssClass="btn btn-primary spaced-btn" Text='<%# CountReview(Eval("id").ToString()) %>' CommandArgument='<%# Eval("id").ToString() %>' CommandName="ReviewDeck"/>

                        <asp:LinkButton ID="learn" runat="server" CssClass="btn btn-info spaced-btn" Text='<%# CountLearn(Eval("id").ToString()) %>' CommandArgument='<%# Eval("id").ToString() %>' CommandName="LearnDeck"/>

                        <asp:LinkButton ID="customize" runat="server" CssClass="btn btn-warning spaced-btn" Text="Customize cards" CommandArgument='<%# Eval("id").ToString() %>' CommandName="CustomizeDeck"/>

                        <asp:LinkButton CssClass="btn btn-dig spaced-btn" ToolTip="Edit" CommandName="Edit" runat="server"><span class="glyphicon glyphicon-pencil"></span>&nbsp; Edit Deck</asp:LinkButton>
                    </div>
                </div>
                </div>
            </div>
        </SelectedItemTemplate>
        <EmptyDataTemplate>
            <div class="container sm-field">
                <h3>No decks here... Perhaps create some?</h3>
            </div>
        </EmptyDataTemplate>
        <EditItemTemplate>
            <div class="col-md-4">
                <div class="item-deck">
                <div class="panel panel-default spanel">
                    <div class="panel-heading" id="phead" runat="server">
                        <asp:TextBox autocomplete="off" ID="nameEdit" CssClass="form-control input" Text='<%# Eval("name") %>' MaxLength="50" runat="server" />
                    </div>
                    <div class="panel-body sbody">
                        <asp:TextBox autocomplete="off" ID="descriptionEdit" CssClass="form-control input no-resize" Text='<%# Eval("description") %>' TextMode="MultiLine" Rows="5" Columns="30" MaxLength="100" runat="server" />
                        <br />
                        <div class="langs">
                        <asp:DropDownList ID="languageEdit" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem value="misc">Select First Language</asp:ListItem>
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
                        <asp:DropDownList ID="languageEdit2" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem value="misc">Select Second Language</asp:ListItem>
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
                    </div>
                    <br />
                    <div class="panel-footer sfooter slide">
                        <div class="progress hide">
                            <div class="progress-bar progress-bar-success black pad-sm" id="deckBar" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width:40%">
                            <%# Eval("progress") + "% Complete" %>
                            </div>
                        </div>
                        <asp:LinkButton CssClass="btn btn-warning spaced-btn" ToolTip="Update" CommandName="Update" runat="server"><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp; Update</asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-danger spaced-btn" OnClientClick="return confirm('Are you sure you want to delete this deck?')" ToolTip="Delete" CommandName="Delete" runat="server"><span class="glyphicon glyphicon-trash"></span>&nbsp; Delete</asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-default spaced-btn" ToolTip="Cancel" CommandName="Cancel" runat="server"><span class="glyphicon glyphicon-remove"></span>&nbsp; Cancel</asp:LinkButton>
                    </div>
                </div>
                </div>
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="col-md-4">
                <div class="item-deck">
                <div class="panel panel-default ipanel">
                    <div class="panel-heading ihead parentCenter" id="phead" runat="server">
                        <asp:TextBox autocomplete="off" placeholder="Deck Name" ID="nameInsert" CssClass="form-control input-sm" MaxLength="50" runat="server" />
                    </div>
                    <div class="panel-body sbody parentCenter">
                        <div>
                        <asp:TextBox autocomplete="off" ID="descriptionInsert" placeholder="Deck Description" CssClass="form-control input-sm no-resize" TextMode="MultiLine" Rows="5" Columns="30" MaxLength="100" runat="server" />
                        <br />
                        <div class="langs">
                        <asp:DropDownList ID="languageInsert" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem value="misc">Select First Language</asp:ListItem>
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
                        <asp:DropDownList ID="languageInsert2" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem value="misc">Select Second Language</asp:ListItem>
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
                        </div>
                    </div>
                    <div class="panel-footer sfooter parentCenter">
                         <asp:LinkButton CssClass="btn btn-success spaced-btn" ToolTip="Add" CommandName="Insert" runat="server"><span class="glyphicon glyphicon-plus"></span>&nbsp; Add deck</asp:LinkButton>
                    </div>
                </div>
                </div>
            </div>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>

