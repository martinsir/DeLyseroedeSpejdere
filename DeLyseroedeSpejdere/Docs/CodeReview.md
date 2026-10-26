# Code Review – godkendelsesark

**Status:** Afventer en reel anden reviewers godkendelse.

| Kontrolpunkt | Kode/afklaring | Status |
|---|---|---|
| Repository Pattern – SQL ude af PageModels | `Repositories/` | Gennemse |
| Interfaces og dependency injection | `I*Repository` og `Program.cs` | Gennemse |
| SQL injection – parameteriserede forespørgsler | `Repositories/*.cs` | Gennemse |
| Login og roller håndhæves på serveren | `Program.cs`, `Pages/Leader/`, `Pages/Scout/` | Gennemse |
| Password-hash, cookie-signering, logout | `Login.cshtml.cs`, `DemoDataSeeder.cs` | Gennemse |
| Ingen dobbelt tilmelding/overbooking | `RegistrationRepository.JoinAsync` | Gennemse |
| Inputvalidering og konsekvent fejlvisning | `ActivityInput.cs`, `Pages/` | Gennemse |
| Unit tests og håndtering af edge cases | `Tests/` | Kør/gennemse |
| Kodestil, navngivning, kommentarer | Alle `.cs` | Gennemse |
| Klassediagram matcher aktuelle klasser | `Klassediagram.png` | Gennemse |

**Reviewer:** ______________________

**Dato:** ______________________

**Filer/commit gennemgået:** ______________________

**Kommentarer og eventuelle rettelser:** ______________________

**Godkendt:** [ ] Ja  [ ] Nej – afventer rettelser

> Dette ark er en anmeldelsesskabelon; der er ikke foretaget en uafhængig godkendelse.
