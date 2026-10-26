# Testplan – De Lyserøde Spejdere

Udfyld **Resultat** i Visual Studio efter hver konkret afprøvning. Ingen af disse tests er markeret bestået på forhånd.

| ID | Testhandling | Forventet resultat | Resultat |
|---|---|---|---|
| T01 | Start appen med database og demo-data | Aktivitetsoversigt vises | Ikke udført |
| T02 | Log ind med `leder` / forkert kode | Login afvises | Ikke udført |
| T03 | Log ind med `leder` / korrekt kode | Lederpanel vises | Ikke udført |
| T04 | Opret en aktivitet | Aktiviteten vises på forsiden | Ikke udført |
| T05 | Opret en aktivitet med slut før start | Valideringsfejl | Ikke udført |
| T06 | Log ind som `spejder`; tilmeld en aktivitet | Tilmelding gemmes | Ikke udført |
| T07 | Tilmeld samme aktivitet to gange | Den anden tilmelding afvises | Ikke udført |
| T08 | Log ind som `junior`; vælg ridning (min. 12 år) | Tilmelding afvises | Ikke udført |
| T09 | Log ind som `junior`; vælg førstehjælpstegn-aktivitet | Tilmelding afvises | Ikke udført |
| T10 | Se Mine tilmeldinger | Kun egne tilmeldinger vises | Ikke udført |
| T11 | Frameld aktivitet | Tilmelding slettes, plads frigives | Ikke udført |
| T12 | Åbn `/Leader/Create` som spejder | Adgang nægtes | Ikke udført |
| T13 | Se deltagerliste som leder | Navne og alder vises | Ikke udført |
| T14 | Rediger aktivitet og slet den igen | Data ændres/slettes | Ikke udført |

Testmiljø: Visual Studio, .NET 10, lokal MySQL. Notér evt. fejl og rettelser, inden afleveringen sendes.

## Supplerende DoD-kontroller

- **UT01–UT08 (unit tests):** Kør `dotnet test DeLyseroedeSpejdere.sln` (eller Visual Studio Test Explorer). Koden ligger i `Tests/DeLyseroedeSpejdere.Tests/`. Registrér antal beståede/fejlede tests efter faktisk kørsel.
- **UI01:** Kontrollér navigation, fokusmarkering, fejlfeedback og mobilvisning efter `UI_Heuristik_og_Gestalt.md`.
- **BT01–BT03:** Gennemfør `Brugertest_3_personer.md` med tre forskellige testpersoner. Skriv de faktiske observationer.
- **CR01:** Få en anden person til at udfylde og godkende `CodeReview.md`.
- **VS01:** Lav reelle Git-commits og udfyld `Sporbarhed.md`.

Status pr. udarbejdelse: **ikke kørt / afventer lokal verificering**.
