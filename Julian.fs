// Learn more about F# at http://fsharp.org
namespace PrayerTimes
open System

module Julian =
//Gregorian to Julian
//Ref: Astronomical Algorithms by Jean Meeus
let adjustMonthByMonth month =
  if month <= 2.0 then
    month + 12.0
  else
    month

let adjustYearByMonth year month =
  if month <= 2.0 then
    year - 1.0
  else
    year

let yearToCenturies year =
  floor (year/100.0)

//double JulianDay (int day, int month, int year, double UT) {
//  if (month<=2) {month=month+12; year=year-1;}
//  return floor(365.25*year) + floor(30.6001*(month+1)) - 15 + 1720996.5 + day + UT/24.0
//}
//
//julian: function(year, month, day) {
//	if (month <= 2) {
//		year -= 1;
//		month += 12;
//	};

//	return JD;
//},
let gregorianToJulian year month day =
  let year = double year;
  let month = double month;
  let day = double day;

  let adjustedYear = adjustYearByMonth year month
  let adjustedMonth = adjustMonthByMonth month
  // I don't know what B is and at this point I'm too afraid to ask
  let centuries = yearToCenturies adjustedYear
  let quadcenturies = floor (centuries/ 4.0)
  let B = 2.0- centuries + quadcenturies
  let julianDate = (floor (365.25*(adjustedYear+ 4716.0)))+ (floor (30.6001*(adjustedMonth+ 1.0)))+ day+ B- 1524.5;
  julianDate

[<EntryPoint>]
let main argv =
  printf "Gregorian to Julian: %f\n" (gregorianToJulian 2019 05 17)
  //printf "Declination: %f\n" (sunRightDeclination (sunMeanEclipticObliquity (daysSinceEpoch 2451545.0)) (sunApparentEclipticLongitude ((daysSinceEpoch >> meanSunLongitude) 2451545.0) ((daysSinceEpoch >> meanSunAnomaly) 2451545.0)))
  //printf "Equation of Time: %f\n" (equationOfTime ((daysSinceEpoch >> meanSunLongitude) 2451545.0) (sunRightAscension (sunMeanEclipticObliquity (daysSinceEpoch 2451545.0)) (sunApparentEclipticLongitude ((daysSinceEpoch >> meanSunLongitude) 2451545.0) ((daysSinceEpoch >> meanSunAnomaly) 2451545.0))))
  //printf "Angular semi-diameter: %f\n" (sunAngularSemiDiameter (sunDistanceFromEarth ((daysSinceEpoch >> meanSunAnomaly) 2451545.0)))
  0 // return an integer exit code
