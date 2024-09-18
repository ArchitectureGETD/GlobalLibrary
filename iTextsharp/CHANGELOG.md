# Changelog

All notable changes to project iTextSharp will be documented in this file.

[**Original Source**](https://github.com/itext/itextsharp)

## [Unreleased]

### Added
- Added signing of the assembly with `itextsharp.GE.snk`.
- Imported `VersionInfo.proj` for versioning.
- Added package references:
  - `Portable.BouncyCastle` version `1.8.10`
  - `System.Drawing.Common` version `6.0.0`
  - `System.Security.Cryptography.Xml` version `6.0.1`
  - `System.Text.Encoding` version `4.3.0`
  - `System.Text.Encoding.CodePages` version `6.0.0`

### Changed
- Changed project setup with .NET 5.0 target framework.
- System.Text.Encoding.Encoding and System.Text.Encoding.CodePages nugets are added to be able to use the Windows-1252 encoding in .NET Core.
- Renaming of namespaces and projects to avoid conflicts with the original package

### Deprecated
- None

### Removed
- None

### Fixed
- None

### Security
- None

