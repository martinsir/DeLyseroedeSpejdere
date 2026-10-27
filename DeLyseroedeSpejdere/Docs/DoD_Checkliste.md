# Definition of Done – De Lyserøde Spejdere

**Projektperiode:** 19.–30. oktober 2026  
**Statusdato:** 27. oktober 2026

## Krav, dokumentation og status

| Nr. | Krav | Dokumentation | Status |
|---|---|---|---|
| 1 | Repository Pattern | `Repositories/`, `Program.cs` | Implementeret; projekterne kompileret og centrale funktioner testet. |
| 2 | Jakob Nielsens relevante heuristikker | `UI_Heuristik_og_Gestalt.md` | Intern gennemgang af alle ti heuristikker udført. UX-01 er registreret og rettet lokalt. |
| 3 | Gestaltlove og visuelt hierarki | `UI_Heuristik_og_Gestalt.md`, `wwwroot/css/site.css` | Gennemgået og dokumenteret. Mobilvisning og kontrast er ikke fuldt testet. |
| 4 | Passende interfaces | `IActivityRepository`, `IUserRepository`, `IRegistrationRepository`, DI | Implementeret; build bestået. |
| 5 | Opdateret klassediagram | `Klassediagram.png`, `Klassediagram.dot` | Udarbejdet; endelig sammenligning med alle relevante klasser udestår. |
| 6 | Code Review godkendt | `CodeReview.md` | Individuel teknisk gennemgang dokumenteret; uafhængig godkendelse mangler. |
| 7 | Versionsstyring og sporbarhed | GitHub `martinsir/DeLyseroedeSpejdere`, `Sporbarhed.md` | Git og GitHub fungerer. Historik og user stories kan følges i repositoryet. |
| 8 | Brugertest med tre personer | `Brugertest_3_personer.md` | **Gennemført ifølge de registrerede testresultater.** Tre anonyme deltagere udførte samme opgaver A, B og C; alle oplevede AM/PM-problemet. |
| 9 | Unit tests | `Tests/DeLyseroedeSpejdere.Tests` | 9 af 9 bestået i Test Explorer. |

## Funktionelle tests og registrerede fejl

- Aktiviteter, login, oprettelse, redigering, sletning, tilmelding og framelding er manuelt afprøvet.
- T02, T05, T08, T09 og T12 er bestået. MySQL afviste dublet ved T07 med fejl 1062; applikationens gentagne tilmeldingsforsøg mangler særskilt test.
- **CR-01:** Valgfrit færdighedstegn blev behandlet som obligatorisk. `RequiredBadge` er ændret til `string?`; rettet og testet.
- **CR-02:** "Tilmeld mig" blev vist efter tilmelding. Eksisterende tilmeldinger hentes nu med `GetMineAsync()` og vises som "Allerede tilmeldt"; rettet og testet.
- **UX-01:** Alle tre anonyme testpersoner oplevede problemer med AM/PM. `Pages/Leader/Create.cshtml` og `Edit.cshtml` bruger nu separate datofelter og tekstbaserede klokkeslæt i `HH:mm`. Begge formularer er afprøvet lokalt efter rettelsen. **Gentest med de tre deltagere er ikke dokumenteret.**

## Kvalitetsvurdering

Den interne vurdering af Nielsens heuristikker, Gestaltlovene og visuelt hierarki er gennemført og dokumenteret. Oplyste brugertests med tre anonyme personer er registreret, og det fælles UX-problem er fulgt op med en lokal rettelse.

**Den samlede Definition of Done er endnu ikke fuldt opfyldt**, fordi uafhængig Code Review-godkendelse og endelig diagramkontrol fortsat mangler. Enkelte supplerende tests (bl.a. fuldt C#-forløb ved dobbelt tilmelding) mangler også. Se `DoD_og_Verifikation.docx` for detaljeret status.
