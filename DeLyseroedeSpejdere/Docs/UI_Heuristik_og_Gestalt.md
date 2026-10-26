# Brugervenlighed – Nielsens heuristikker og Gestalt

## Jakob Nielsens 10 heuristikker, anvendt på projektets UI

1. **Systemets status er synlig:** `TempData` viser resultat af oprettelse, tilmelding og fejl i `Pages/Shared/_Layout.cshtml` med `role=status/alert`.
2. **Sammenhæng med virkeligheden:** Aktiviteter, ledere, pladser, alder og færdighedstegn beskrives i spejdernes eget sprog.
3. **Brugerkontrol og frihed:** Frameld-knap og Annuller i formularer; bekræftelse før sletning/framelding.
4. **Konsistens og standarder:** Ens knapper, formularelementer, navigationsmenu og kort i hele applikationen.
5. **Forebyggelse af fejl:** Inputvalidering, server-side rollebeskyttelse, kapacitetskontrol og deaktiveret tilmelding ved fuldt hold.
6. **Genkendelse frem for hukommelse:** Krav til aldersgrænser, færdighedstegn, pladser og aktivitetstid er synlige ved aktiviteten.
7. **Fleksibilitet og effektivitet:** Lederpanel har rediger, deltagere og slet samme sted. Navigationsmenu viser relevante rollefunktioner.
8. **Æstetik og minimalistisk design:** Kort med vigtig information, få handlinger og begrænset antal elementer på hver side.
9. **Hjælp til at genkende og rette fejl:** Valideringsbeskeder ved ugyldig aktivitet og tydelige fejl ved afvist tilmelding.
10. **Hjælp og dokumentation:** `README.md` forklarer opstart/testkonti; UI bør suppleres med en kort Hjælp-side ved reel brug.

## Gestaltlove, anvendt

- **Nærhed:** Oplysninger og handlinger for samme aktivitet er samlet i ét kort (`.card`).
- **Lighed:** Samme udtryk for knapper, formularfelter, overskrifter og aktivitetskort på tværs af siderne.
- **Fælles region:** Aktivitetskort og formularpaneler markerer beslægtet indhold med ramme og baggrund.
- **Kontinuitet:** Genkendelig topnavigation og placering af handlinger på tværs af sider.
- **Figur/baggrund:** Hvide kort på lys baggrund giver hierarki og læsbarhed.

Dette er **en dokumenteret designgennemgang**, ikke dokumentation for bestået brugertest. Se `Brugertest_3_personer.md`.
