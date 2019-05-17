// Learn more about F# at http://fsharp.org
namespace PrayerTimes
open System

module SunCalcs =
//Suns angular coordinates
// daysSinceJulianEpoch = days since January 1, 4713 BC
// https://aa.usno.navy.mil/faq/docs/SunApprox.php

//   daysSinceEpoch = daysSinceJulianEpoch - 2451545.0;  // daysSinceJulianEpoch is the given Julian date, epoch here is 2003 Gregorian

let daysSinceEpoch daysSinceJulianEpoch =
  daysSinceJulianEpoch - 2451545.0

//   g = 357.529 + 0.98560028* daysSinceEpoch;
let meanSunAnomaly daysSinceEpoch =
  357.529 + 0.98560028 * daysSinceEpoch
//   q = 280.459 + 0.98564736* daysSinceEpoch;
let meanSunLongitude daysSinceEpoch =
  280.459 + 0.98564736 * daysSinceEpoch
//   L = q + 1.915* sin(g) + 0.020* sin(2*g);
let sunApparentEclipticLongitude meanSunLongitude meanSunAnomaly =
  meanSunLongitude + 1.915 * (sin meanSunAnomaly) + 0.020 * (sin 2.0 * meanSunAnomaly)
//   R = 1.00014 - 0.01671* cos(g) - 0.00014* cos(2*g);
let sunDistanceFromEarth meanSunAnomaly =
  1.00014 - 0.01671 * (cos meanSunAnomaly) - 0.00014 * (cos 2.0*meanSunAnomaly)
//   e = 23.439 - 0.00000036* daysSinceEpoch;
let sunMeanEclipticObliquity daysSinceEpoch =
  23.439 - 0.00000036 * daysSinceEpoch
//   RA = atan2(cos(e)* sin(L), cos(L))/ 15;
let sunRightAscension sunMeanEclipticObliquity sunApparentEclipticLongitude =
  (atan2
    ((cos sunMeanEclipticObliquity)*(sin sunApparentEclipticLongitude))
    (cos sunApparentEclipticLongitude)
  ) / 15.0

//   D = asin(sin(e)* sin(L));  // declination of the Sun
let sunRightDeclination sunMeanEclipticObliquity sunApparentEclipticLongitude =
  asin (sin sunMeanEclipticObliquity) * (sin sunApparentEclipticLongitude)
//   EqT = q/15 - RA;  // equation of time
let equationOfTime meanSunLongitude sunRightAscension =
  meanSunLongitude / 15.0 - sunRightAscension
//   SD = 0.2666 / R
let sunAngularSemiDiameter sunDistanceFromEarth =
  0.2666 / sunDistanceFromEarth
