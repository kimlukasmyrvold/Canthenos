# Changelog

All notable changes to this project will be documented in this file.

The format for this file is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).<br>
Dates use the [ISO 8601 date format](https://www.iso.org/iso-8601-date-and-time-format.html) of `YYYY-MM-DD`.

## [Unreleased]

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

[unreleased]: https://github.com/kimlukasmyrvold/Canthenos/compare/v0.2.0...HEAD
[0.2.0]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.2.0
[0.1.0]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.1.0
[0.0.1]: https://github.com/kimlukasmyrvold/Canthenos/releases/tag/v0.0.1
