# Brugertest – De Lyserøde Spejdere

## 1. Formål

Formålet med brugertesten er at undersøge, om applikationens brugergrænseflade er forståelig, og om de centrale funktioner kan gennemføres uden tekniske fejl.

Testen tager udgangspunkt i tre faste opgaver, der dækker aktivitetsoversigt, spejderfunktioner og lederfunktioner.

**Testmiljø:** ASP.NET Core Razor Pages, Firefox og MySQL.

**Testdato:** 26. oktober 2026.

## 2. Testopgaver

**Opgave A – Aktivitetsoversigt**

Find aktiviteten "Mad over bål", og undersøg tidspunktet og antallet af ledige pladser.

**Opgave B – Spejder**

Log ind som spejder, tilmeld en aktivitet, og find den under "Mine tilmeldinger".

**Opgave C – Leder**

Log ind som leder, opret en ny aktivitet, og find de tilmeldte deltagere på en aktivitet.

## 3. Testperson 1 – Intern funktionstest

**Dato:** 26-10-2026  
**Tester:** Projektudvikler

| Opgave | Gennemført? | Observationer og forbedringer |
|---|---|---|
| A | Ja | Aktivitetsoversigten viste navn, tidspunkt og ledige pladser. |
| B | Ja | Tilmelding og visning af egne aktiviteter fungerede. Knappen "Tilmeld mig" blev først ikke ændret efter tilmelding. |
| C | Ja, efter rettelse | Oprettelse af aktivitet fejlede først, fordi et valgfrit færdighedstegn blev behandlet som obligatorisk. Efter rettelsen fungerede oprettelse og deltageroversigt. |

## 4. Testperson 2

**Dato:** Ikke gennemført  
**Tester:** Afventer

| Opgave | Gennemført? | Observationer og forbedringer |
|---|---|---|
| A | Afventer | Ikke testet |
| B | Afventer | Ikke testet |
| C | Afventer | Ikke testet |

## 5. Testperson 3

**Dato:** Ikke gennemført  
**Tester:** Afventer

| Opgave | Gennemført? | Observationer og forbedringer |
|---|---|---|
| A | Afventer | Ikke testet |
| B | Afventer | Ikke testet |
| C | Afventer | Ikke testet |

## 6. Opsamling

**Hvad fungerede godt?**

Aktivitetsoversigten var tilgængelig, og login som både spejder og leder fungerede. Tilmeldinger kunne gemmes og vises, og lederen kunne oprette, redigere og slette aktiviteter samt se deltagere.

**Hvad var uklart eller problematisk?**

Der blev identificeret to problemer:

- Et valgfrit færdighedstegn blev behandlet som obligatorisk ved oprettelse.
- Aktivitetsoversigten viste fortsat "Tilmeld mig" efter en gennemført tilmelding.

**Ændringer efter test:**

- CR-01: `RequiredBadge` blev ændret til `string?`.
- CR-02: Aktivitetsoversigten blev ændret til at vise "Allerede tilmeldt" for eksisterende tilmeldinger.

Begge rettelser blev efterfølgende testet manuelt.

**Samlet status:**

Den interne funktionstest er gennemført. Brugertest med tre forskellige, uafhængige personer udestår fortsat i forhold til Definition of Done.