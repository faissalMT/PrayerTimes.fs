namespace Tests
open NUnit.Framework
open PrayerTimes.Julian

[<TestClass>]
type TestClass () =

  [<SetUp>]
  member this.Setup () =
    ()

  [<Test>]
  member this.Test1 () =
    Assert.AreEqual((gregorianToJulian 2019 5 17), 2458620.5)
