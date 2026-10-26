# Sporbarhed og versionsstyring

Projektets kildekode afleveres med `.gitignore`, men **koden i ZIP er ikke dokumentation for faktiske Git-commits**. Opret Git-repository og upload projektets kildekode med mindst ét reelt commit. Registrer kun faktiske datoer.

| User story | Relevante filer | Test |
|---|---|---|
| US-01 Aktivitetsoversigt | `Pages/Activities/Index.*`, `Repositories/ActivityRepository.cs` | T01 |
| US-02 Login og roller | `Pages/Account/*`, `Program.cs`, `Repositories/UserRepository.cs` | T02, T03, T12 |
| US-03 Opret aktivitet | `Pages/Leader/Create.*`, `Models/ActivityInput.cs` | T04, T05 |
| US-04 Tilmeld aktivitet | `Repositories/RegistrationRepository.cs`, `Services/RegistrationRules.cs` | T06–T09 |
| US-05 Mine tilmeldinger | `Pages/Scout/Mine.*` | T10 |
| US-06 Framelding | `Pages/Scout/Mine.*`, `Repositories/RegistrationRepository.cs` | T11 |
| US-07 Ændre/slette | `Pages/Leader/Edit.*`, `Pages/Leader/Index.*` | T14 |
| US-08 Deltagerliste | `Pages/Leader/Participants.*` | T13 |
| US-09 Flere ledere | Ikke implementeret | Ikke testet |

## Git-procedure

Åbn rodmappen i terminalen:

```bash
git init
git add .
git commit -m "Initial: Razor Pages scout activities and SCRUM docs"
```

Opret derefter et GitHub-repository og tilknyt remote i Visual Studio. Når noget ændres, angiv story-ID i commit-beskeden, fx `US-04: valider alder og færdighedstegn`. Angiv reelle commits under.

| Dato | Commit-ID | US-ID | Ændring |
|---|---|---|---|
| | | | |
| | | | |

**Bemærk:** Den officielle opgaveperiode 19.–30. oktober angiver projektets ramme og er ikke i sig selv dokumentation for, hvornår udviklingsarbejdet blev udført.
