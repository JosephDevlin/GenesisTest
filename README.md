GenesisTest App

Assumptions

- Minimum android version is 5.0
- Logging/crash reporting etc not required

TODO

- Implement username search with the repository name search
- Create common styles to apply in the app (instead of manually adding attributes everywhere)
- Add docs
- Revise project structure and codebase
- Add Unit Tests
- Implement local cache
- Cancel get pull requests task if user navigates back
- Adjust the stars count on repository item cell template so that it looks better in landscape
- Review linker to see if we can make the app smaller

BUGS

- When getting the repositories some of the most popular items are missing from the first page
- When PR screen is loading it should hide open/closed summary with loading indicator too
- Stars count too far to the right when in landscape mode
