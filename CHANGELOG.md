# Changelog

All notable changes to this project will be documented in this file.

The format for this file is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).<br>
Dates use the [ISO 8601 date format](https://www.iso.org/iso-8601-date-and-time-format.html) of `YYYY-MM-DD`.

## [0.3.1] - 2024-04-06

Minor update

### Added

- Check for when github version was last checked (should help prevent overloading the api)
- methods for accessing localstorage
- methods for accessing sessionstorage

## [0.3.0] - 2024-04-02

This will be another mayor release with lots of changes as well as new things.

### Added

- cancel button styling
- new button styling for checkbox
- new button styling for radio
- new component for password toggle button
- new models for creating new user
- component for viewing all users from database
- new folder for login related components
- components for creating new users
- Brand, BrandLogo component
- global theme file, this will store static colors
- destructive color to global theme file
- own scss component for table
- .env template file
- website version to bottom of footer
- license info to footer
- class for managing .env file
- class for handling of cookies (read/write)
- file for getting the latest github version for this project
- custom styling for scrollbar
- styling for information container
- styling for page not found
- images for dishes, drinks and snacks
- component for loading circle
- component for dropdowns

### Changed

- split up BrandNameLogo component into three, one for just the brand name, one just for the brand logo and the third with both combined
- naming of menus due to overhaul to database server. Column names had changed so the previous would no longer work.
- almost entire database, should now be able to add orders so it will be possible to count the amount of times a product ha been bought.
- class naming of LoginForm component
- styling for table, should be more responsive on smaller screens
- moved connection string from appsettings.json to .env
- increased max width of footer
- moved code behind section of footer down to copyright section
- wrapper class, instead of putting wrapper on each element it is now on <main> element
- renamed MainLayout.razor to Main.razor

### Removed

- old table styling from _base.scss component
- appsettings.template.json
- gitignore for appsettings.json


## [0.2.0] - 2024-02-25

This release makes the site actually connect to a database. It also brings in some more pictures and styling improvements.

### Added

- new dish images
- class libraries for connection to database in DataAccessLibrary layer
- transients for connection up Web layer to DataAccessLibrary layer

### Changed

- Menu components, now connects to database and gets the information from there
- scroll-padding-top from 50px to 60px due to navbar height change
- spacing-inline css variable, made values bigger
- styling for dish- and weeklyMenu component so it looks better and is more responsive
- appsettings.template.json to include template to connectionString for connection to database

### Removed

- old pepperoni pizza dish image

## [0.1.0] - 2024-02-25

### Added

- favicons, added one for dark, light and a default one.
- favicon to _Layout.cshtml
- script in header for changing favicon theme (dark / light)
- component for website brand, the logo and name
- logo to header
- logo to footer

### Changed

- link to this github repo to proper link instead of placeholder link
- styling of footer `&__brand`
- made weekly menu container center the dishes
- decreased max width of dish component from 300px to 250px
- height of header
- branding section of header and footer, now uses new branding component

## [0.0.1] - 2024-02-24

### Added

- This Changelog file
- README file
- GPL3.0 license 

[unreleased]: https://github.com/kimlukasmyrvold/Canthenos/compare/v0.3.1...HEAD
[0.3.1]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.3.1
[0.3.0]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.3.0
[0.2.0]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.2.0
[0.1.0]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.1.0
[0.0.1]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.0.1
