GenesisTest App

Assumptions

	- Minimum android version is 5.0
	- Logging/crash reporting etc not required
	- The useragent im sending to github does not need to be secured (i can leave it in code, visible on github)

TODO

	- Implement username search with the repository name search
	- Create common styles to apply in the app (instead of manually adding attributes everywhere)
	- Add docs
	- Revise project structure and codebase
	- Add Unit Tests (the application has been built to be testable but the time was not there to get this done)
	- Implement local cache in case app goes offline or activities are dumped etc (prob use local file as sql may be overkill for take home test)
	- Cancel get pull requests task if user navigates back
	- Adjust the stars count on repository item cell template so that it looks better in landscape
	- Review linker to see if we can make the app smaller
	- Display an error/toast on the screen if an error happen getting the data

BUGS

	- When getting the repositories some of the most popular items are missing from the first page
	- When PR screen is loading it should hide open/closed summary with loading indicator too
	- Stars count too far to the right when in landscape mode
	- Duplicate items appearing on list when paging


APPLICATION ARCHITECTURE

API Layer

	- Chose to use Refit as it is very easy and fast to set up API integration logic within the app. I integrated refit with the built in Xamarin Forms dependency injection so the services consuming the API would not have their testability compromised.
	- For speed of development purposes I pasted the json responses from github into a json -> C# converter so there are a lot of poorly named DTOs sitting with the refit interfaces. I felt this was fine as it is only a take home test and not a production app.
	- I did not have time to integrate a library like Polly into the API calls to make them robust. As it stands any error in the app will potentially crash it or just fail to return data.

Service Layer

	- I created a service layer that will be responsible for return mapped data that was retrieved from the API. 
	- I would have added caching at this level if I had time. I actually had another branch in this repo where I implemented full caching with paging support using Akavache but when I was done I felt that the solution was sub optimal and did not add much value given that it was only an in memory cache so I did not merge. 
	- In order to get the summary information for open/closed pull requests I had to pull a little trick. Github handles their paging via page links to next,last etc. They do not give summary information like counts so I had to use a bit of regex (badly) to read those links and calculate the total amount of results or a given search.
	- I did not build in any error handling as I prioritised getting the features working. I would have implemented the cachine at this level also.
	
ViewModel Layer

	- I created a view model for each feature and implemented them in strict MVVM fashion.
	- I chose to use MvxNotifyTask here as it is very easy and clean to get information about data retrieval and bind to it (loading animation on list view)
	

View Layer

	- I do not believe the layouts in the xaml files are optimal as I have never used xamarin forms before. I tried to keep the nesting to a minimum but I really miss having a control like the constraint layout in android to work with. As such I feel that the performance of the list view items might not be the best.
	- For paging I googled around on how to do it in Xamarin Forms and practically every answer was to do it via a behaviour on the listview. The code for that behaviourclass is not my own but the product of advice and samples from around the web mixed with tweaks based on my own requirements.
	- I chose to use SVG icons in the repositories UI as it was very fast to setup and easy to alter their colour on the UI.
	- For the splash screen I literally took a snip of the screen from the spec and saved it as a PNG. It looks a little off when loading but it was fast to do and I was not going to spend a ton of time trying to get that optional item perfect.
	- I did not have time to extract the common styling out as static resources. If I did I would have extracted the text, colour and layout styling for various controls, stored them in FormsApp.xaml along with the converters and referenced them in the UI.
	- I did not put the hamburger menu icon on the repositories screen as there was no functionality associated with it. The app has no navigation and I felt no button was better than a non functional one. 
	- I think I took a shortcut when colouring the toolbar as I set it directly in the android project. The alternative seemed to be a lot more infrastructure on this simple app to get access to the toolbar. I chose this simple solution rather than all that overhead.