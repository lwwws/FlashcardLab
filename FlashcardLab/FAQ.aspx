<%@ Page Title="FAQ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="FlashcardLab.FAQ" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/default.css" />

    <div class="jumbotron bfield">
        <h1 class="smoke txt-logo">Frequently Asked Questions</h1>
    </div>

    <section class="accordion-section">
        <div class="container">
  	        <div class="panel-group" id="accordion" aria-multiselectable="true">
		        <div class="panel txt-light">
		            <div class="panel-heading" id="q0">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c0" aria-expanded="true" aria-controls="c0">
				            What is FlashcardLab?
			                </a>
			            </h1>
		            </div>
		            <div id="c0" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q0">
			            <div class="panel-body">
                            FlashcardLab is a website that provides you tools to learn languages through flashcards.
			            </div>
		            </div>
		        </div>

                <div class="panel txt-light">
		            <div class="panel-heading" id="q1">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c1" aria-expanded="true" aria-controls="c1">
				            How do I start studying?
			                </a>
			            </h1>
		            </div>
		            <div id="c1" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q1">
			            <div class="panel-body">
                            You need to create your own account <a href="Register.aspx">here</a>, and then in your decks section you can create a new deck, add cards to it accordingly and study them!
                            Alternatively, if you dont want to make your own, you can download one from the store.
			            </div>
		            </div>
		        </div>

                <div class="panel txt-light">
		            <div class="panel-heading" id="q2">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c2" aria-expanded="true" aria-controls="c2">
				            What is "Share with others" checkmark mean in the deck customization?
			                </a>
			            </h1>
		            </div>
		            <div id="c2" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q2">
			            <div class="panel-body">
                            It means it will appear on the store of shared decks and other users would be able to download it and leave a comment, rate it, etc.
                            You can always remove it from being shared by removing the checkmark.
                            It is worth noting that, any changes that you make in your deck after its been shared are still present in the store. Users who download it get a copy of it.
			            </div>
		            </div>
		        </div>

                <div class="panel txt-light">
		            <div class="panel-heading" id="q3">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c3" aria-expanded="true" aria-controls="c3">
				            What is store/shared decks?
			                </a>
			            </h1>
		            </div>
		            <div id="c3" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q3">
			            <div class="panel-body">
                            It is a place where people share their own decks for free with others, so that you can download them and study if you ont want to make your own.
			            </div>
		            </div>
		        </div>

                <div class="panel txt-light">
		            <div class="panel-heading" id="q4">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c4" aria-expanded="true" aria-controls="c4">
				            What are levels? What's the difference between learn and review?
			                </a>
			            </h1>
		            </div>
		            <div id="c4" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q4">
			            <div class="panel-body">
                            Each card that you make has its own level, ranging from 0 to 9. You start from 0, which means you haven't seen this card or you don't know it at all. Level 0 cards will appear in learn as cards
                            that are new to you. Levels from 1-8 are how good you know and remember the card, the higher it is the more you are familiar with it - those are the cards that appear in review, so you will study them better.
                            Level 9 means you know the card by heart. It will no longer appear in learn or review.
			            </div>
		            </div>
		        </div>

                <div class="panel txt-light">
		            <div class="panel-heading" id="q5">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c5" aria-expanded="true" aria-controls="c5">
                            How does it work? How do you increase/decrease level of the card?
			                </a>
			            </h1>
		            </div>
		            <div id="c5" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q5">
			            <div class="panel-body">
                            Each time you study the cards, the algorithm sets a level according to how many mistakes you've made, according to the level, it sets the next review or puts it in learn for level 0.
			            </div>
		            </div>
		        </div>

                <div class="panel txt-light">
		            <div class="panel-heading" id="q6">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c6" aria-expanded="true" aria-controls="c6">
                            What is "Study both sides" checkmark mean in the customization?
			                </a>
			            </h1>
		            </div>
		            <div id="c6" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q6">
			            <div class="panel-body">
                            By default, the front of the card is shown and you have to type the back of the card, but if you want to study from both of the sides you can put on this checkmark.
                            This will cause showing the front, typing the back and vice versa for each card in a session.
			            </div>
		            </div>
		        </div>



                <div class="panel txt-light">
		            <div class="panel-heading" id="q7">
			            <h1 class="panel-title">
                            <span class="glyphicon glyphicon-question-sign"></span>
			                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#c7" aria-expanded="true" aria-controls="c7">
                            Can I add multiple answers to the back?
			                </a>
			            </h1>
		            </div>
		            <div id="c7" class="panel-collapse collapse smoke" role="tabpanel" aria-labelledby="q7">
			            <div class="panel-body">
                            Yes! Just write accepted back answers divided by commas. For example: ' Hello, Greetings, Hi '. Remember that checking is not case sensitive, so it doesnt matter if you write answer with upper case or lower case letters.
			            </div>
		            </div>
		        </div>




            </div>
        </div>
    </section>



</asp:Content>
