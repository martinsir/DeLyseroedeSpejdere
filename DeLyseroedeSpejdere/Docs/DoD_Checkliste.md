# Definition of Done – De Lyserøde Spejdere

**Kilde:** Underviserens `DoD.docx`. Punkterne nedenfor er overført uden at udvande kravene. **Status er ikke det samme som godkendelse.**

| Nr. | Krav fra DoD | Dokumentation i projektet | Status pr. klargøring |
|---|---|---|---|
| 1 | Overholde Repository Pattern | `Repositories/` med SQL isoleret fra PageModels | Implementeret i kode – kræver kørsel/kodegennemgang |
| 2 | Overholde Jakob Nielsens 10 heuristika (relevante) | `Docs/UI_Heuristik_og_Gestalt.md`, Razor Pages og CSS | Designet med relevante principper – brugergennemgang mangler |
| 3 | Overholde Gestalt love | `Docs/UI_Heuristik_og_Gestalt.md`, CSS-grid og formulargrupper | Implementeret som designvalg – vurdering mangler |
| 4 | Passende Interfaces implementeres | `IActivityRepository`, `IUserRepository`, `IRegistrationRepository` og DI i `Program.cs` | Implementeret i kode – kompilering mangler |
| 5 | Klassediagram opdateret | `Docs/Klassediagram.png` og `Docs/Klassediagram.dot` | Udarbejdet – bør kontrolleres imod seneste kode |
| 6 | Skal være godkendt i Code Review | `Docs/CodeReview.md` | **Afventer godkendelse fra en anden person** |
| 7 | Skal overholde versionsstyring og sporbarhed | `.gitignore`, `Docs/Sporbarhed.md`, GitHub-repo skal kobles på | **Afventer faktiske Git-commits / repository** |
| 8 | Skal brugertestes på 3 personer | `Docs/Brugertest_3_personer.md` med testcases og 3 logafsnit | **Afventer 3 reelle brugertests** |
| 9 | Unit Test skal være implementeret | `Tests/DeLyseroedeSpejdere.Tests/` (xUnit) | Testkode skrevet – **afventer kørsel og resultater** |

En user story må først markeres **Done**, når den fungerer og de projektfælles DoD-krav er verificeret. Brug en mellemstatus som *Implementeret – afventer DoD* indtil da.
