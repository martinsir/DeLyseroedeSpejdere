# Brugertest – De Lyserøde Spejdere

**Testdato:** 26. oktober 2026  
**Testmiljø:** Firefox, ASP.NET Core Razor Pages og MySQL  
**Testmetode:** Observation af tre anonyme testpersoner  
**Antal testpersoner:** 3

## 1. Formål

Formålet med brugertesten er at undersøge, om almindelige brugere kan navigere i applikationen og gennemføre de centrale funktioner uden teknisk vejledning.

Alle tre testpersoner blev præsenteret for de samme tre opgaver, så resultaterne kunne sammenlignes.

## 2. Testopgaver

**Opgave A – Aktivitetsoversigt**

Find aktiviteten "Mad over bål", og aflæs tidspunkt samt antal ledige pladser.

**Opgave B – Spejder**

Log ind som spejder, tilmeld en aktivitet, og find den under "Mine tilmeldinger".

**Opgave C – Leder**

Log ind som leder, opret en aktivitet med start- og sluttidspunkt, og find deltageroversigten.

## 3. Testperson 1 – Anonym

| Opgave | Gennemført? | Observationer |
|---|---|---|
| A | Ja | Ingen registrerede vanskeligheder. |
| B | Ja | Ingen registrerede vanskeligheder. |
| C | Ja, med besvær | Dato- og tidsindtastningen var vanskelig at forstå på grund af AM/PM-formatet. |

## 4. Testperson 2 – Anonym

| Opgave | Gennemført? | Observationer |
|---|---|---|
| A | Ja | Ingen registrerede vanskeligheder. |
| B | Ja | Ingen registrerede vanskeligheder. |
| C | Ja, med besvær | AM/PM-formatet skabte usikkerhed ved valg af start- og sluttidspunkt. |

## 5. Testperson 3 – Anonym

| Opgave | Gennemført? | Observationer |
|---|---|---|
| A | Ja | Ingen registrerede vanskeligheder. |
| B | Ja | Ingen registrerede vanskeligheder. |
| C | Ja, med besvær | Dato- og tidsfelterne var vanskelige at anvende, da de ikke viste dansk 24-timers format. |

## 6. Samlet opsamling

| Opgave | Gennemført af | Registrerede vanskeligheder |
|---|---|---|
| A – Aktivitetsoversigt | 3 ud af 3 | Ingen |
| B – Tilmelding | 3 ud af 3 | Ingen |
| C – Lederfunktion | 3 ud af 3 | AM/PM-format ved datoindtastning |

Alle tre testpersoner gennemførte opgaverne. Den eneste fælles vanskelighed var dato- og tidsindtastningen.

Problemet blev observeret hos samtlige tre testpersoner og vurderes derfor som et relevant forbedringspunkt for brugergrænsefladen.

## 7. Registreret forbedringspunkt

### UX-01 – Dato- og tidsindtastning

**Problem:** Dato- og tidsfelterne anvender en visning med AM/PM i stedet for det danske 24-timers format.

**Konsekvens:** Brugerne havde vanskeligt ved at indtaste og kontrollere korrekte tidspunkter.

**Forslag til forbedring:** Tilpas dato- og tidsindtastningen til dansk format med 24-timers klokkeslæt, fx 14:00 i stedet for 02:00 PM.

**Prioritet:** Mellem

**Status:** Registreret – afventer rettelse og efterfølgende test.

## 8. Konklusion

Brugertesten viser, at alle tre testpersoner kunne gennemføre de centrale brugeropgaver.

Navigation, aktivitetsoversigt, tilmelding og lederfunktioner blev anvendt uden andre rapporterede vanskeligheder.

Dato- og tidsindtastningen var det eneste fælles problem og bør prioriteres i den næste forbedring af brugergrænsefladen.

**Samlet resultat:** Brugertest med tre personer gennemført. Ét fælles forbedringspunkt identificeret.