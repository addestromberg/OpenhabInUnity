# OpenHAB In Unity
Simple Openhab to Unity3D integration to build UI's.
Im a hobby developer dipping in all kinds of stuff. Have mostly played around with Unity and VR the last years.
When starting to get into home automation and OpenHAB I got really frustrated when building HabPanel UI's as it felt
really restricted coming from Unity and that UI system. The main source of influence that made me build this little package was 
when I tried to use [pmpkk's](https://community.openhab.org/t/show-current-sun-position-and-shadow-of-house-generate-svg/34764) really cool
Sun and Shadow script for a house model. That got me thinking about the possibilities of what one could do in a full on 3D space game
engine. So I started creating some components in unity to create something similar but rendered in 3d of my house.

I then realized that this could all be applied to whatever item in OpenHab I wanted and I started to build "Widgets" similar to HabPanel.
Be aware, Im a single-, mediocre-, self tought-, hobby programmer. The software should NOT be used in commercial projects. 
I have not spent lot of time thinking about security etc. but mostly just wanted to make things work. 

Anyone interested can make a pull request would be very welcome.

## Installation
Clone or Download the repo to whatever folder you wan't in the Assets folder in your Unity project. I used Unity 2019.1.7f1 to test the
scripts but there is nothing fancy so whatever version you want to use should be ok.

You also need to have the following dependencies in your project:

### Included in package file:
* [Vibrant.InfluxDB.Client](https://github.com/MikaelGRA/InfluxDB.Client/tree/master/src/Vibrant.InfluxDB.Client)
* [EvtSource](https://github.com/3ventic/EvtSource)
* [REST Client for Unity](https://github.com/proyecto26/RestClient)

### Exluded in package file but available for free on AssetStore:
* [JSON.NET for Unity](https://assetstore.unity.com/detail/tools/input-management/json-net-for-unity-11347)

JSON.NET for Unity is needed becouse of the InfluxDB client library depends on Newtonsoft namespace.

## Usage
More info coming...when I can find the time. =)

## Contributing
Pull requests are welcome.

## License
[GNU General Public License 3.0](https://choosealicense.com/licenses/gpl-3.0/)
