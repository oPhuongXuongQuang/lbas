IoTApp.createModule('IoTApp.MapPane', (function () {
    "use strict";

    var self = this;
    var mapApiKey = null;
    var map;
    var pinInfobox;
    var boundsSet = false;

    var init = function () {
        $.ajaxSetup({ cache: false });
        getMapKey();
        startMap();
    }

    var getMapKey = function () {
        // NguyenTN - Modify
        self.mapApiKey = 'AsSleCImzXwfwQAQ8Ykzq4dXmLeppVWJ4xyJwlhLdf1E3V9kaJKGwM5mnvf2IiQ4';
    }

    var startMap = function () {
        Microsoft.Maps.loadModule('Microsoft.Maps.Themes.BingTheme', { callback: finishMap });
    };

    var finishMap = function () {
        var mapOptions = {
            credentials: self.mapApiKey,
            mapTypeId: Microsoft.Maps.MapTypeId.aerial,
            animate: false,
            enableSearchLogo: false,
            enableClickableLogo: false,
            //disable dashboard
            showDashboard: false
        };

        // Initialize the map
        self.map = new Microsoft.Maps.Map(document.getElementById("deviceMap"), mapOptions);

        // Hide the infobox when the map is moved.
        Microsoft.Maps.Events.addHandler(self.map, 'viewchange', hideInfobox);

        //TODO: get devices from cloud
        var devicesLocation = getMockDevicesLocation();
        //TODO: from device location -> bound
        var viewBound = getViewBound();

        // NguyenTN - Modify
        // set device properties: location and stuff on to map
        // we just get the location, the detail properties of device will be get when user click on device on map
        setDeviceLocationData(viewBound.minLatitude, viewBound.minLongitude, viewBound.maxLatitude,
            viewBound.maxLongitude, devicesLocation);
    }

    var setDeviceLocationData = function setDeviceLocationData(minLatitude, minLongitude, maxLatitude,
      maxLongitude, deviceLocations) {

        if (!self.map) { return; }
        //Set view
        if (!boundsSet) {
            var mapOptions;
            boundsSet = true;
            mapOptions = self.map.getOptions();
            mapOptions.bounds = Microsoft.Maps.LocationRect.fromCorners(
                    new Microsoft.Maps.Location(maxLatitude, minLongitude),
                    new Microsoft.Maps.Location(minLatitude, maxLongitude));
            self.map.setView(mapOptions);
        }

        // Set device on map
        self.map.entities.clear();
        if (deviceLocations) {
            for (var i = 0 ; i < deviceLocations.length; ++i) {
                setDevicePin(deviceLocations[i]);
            }
        }
    }

    var setDevicePin = function (deviceLocations) {
        //set device location
        var loc = new Microsoft.Maps.Location(deviceLocations.latitude, deviceLocations.longitude);

        //device - pin properties
        var pinOptions = {
            // pin img prop
            height: 17,
            width: 17,
            visible: true,
            // device prop
            id: deviceLocations.deviceId,
            zIndex: deviceLocations.status
        };

        //pin icon
        switch (deviceLocations.status) {
            case 1:
                pinOptions.icon = resources.cautionStatusIcon;
                break;
            case 2:
                pinOptions.icon = resources.criticalStatusIcon;
                break;
            default:
                pinOptions.icon = resources.allClearStatusIcon;
                break;
        }

        var pin = new Microsoft.Maps.Pushpin(loc, pinOptions);
        Microsoft.Maps.Events.addHandler(pin, 'click', onMapPinClicked);

        self.map.entities.push(pin);
    }

    //==========Begin Mock Data ===========
    var getViewBound = function () {
        return {
            minLatitude: 47.533582,
            minLongitude: -122.382165,
            maxLatitude: 47.709159,
            maxLongitude: -122.080622
        };
    }
    var getMockDevicesLocation = function () {
        return [{ deviceId: "SampleDevice001_724", latitude: 47.583582, longitude: -122.130622, status: 0 }
             , { deviceId: "SampleDevice002_724", latitude: 47.617025, longitude: -122.191285, status: 1 }
             , { deviceId: "SampleDevice003_724", latitude: 47.637025, longitude: -122.111285, status: 2 }
             , { deviceId: "SampleDevice004_724", latitude: 47.627075, longitude: -122.391295, status: 3 }];
    }

    var getMockDeviceInfo = function (e) {
        var deviceStateStr = "Caution";
        var deviceStatezIndex = e.zIndex;

        switch (deviceStatezIndex) {
            case 1: deviceStateStr = "Caution"; break;
            case 2: deviceStateStr = "Critical"; break;
            default: case 2: deviceStateStr = "All clear"; break;
        }
        var temp = Math.floor(Math.random() * 100);
        var hum = Math.floor(Math.random() * 100);
        var info = 
        {
            systemName: "Flexy™ EC Rooftop",
            id: e.target.getId(),
            hubEnableState: "False",
            createdTime: "2015-12-19 22:33:44",
            deviceState: deviceStateStr,
            updatedTime: null,
            manufacturer: "Lennox International Inc",
            modelNumber: "LCC-367",
            serialNumber: "SER8247",
            firmwareVersion: "2.05",
            platform: "Linux-54",
            processor: "QCA9533-AL3A",
            installedRAM: "32 MB",
            longitude:  e.target._location.longitude,
            latitude: e.target._location.latitude,
            corperation: "McDonalds",
            franchise: "McDonalds-FR01",
            siteAddress: "2140 Lake Park Blvd.Richardson, TX 75080",
            systemAddress: "3333 Lake Park Blvd.Richardson, TX 75080",
            building: "Building01",
            zone: "Zone01",
            temperature: temp,
            humility: hum
        }
        return info;
    }
    //==========End Mock Data ===========

    var onMapPinClicked = function (e) {
        // NguyenTN - Modify
        //IoTApp.Dashboard.DashboardDevicePane.setSelectedDevice(e.target.getId());

        var deviceInfo = getDeviceInfo(e);

        displayInfobox(e, deviceInfo);

        // NguyenTN - Modify
        displayDetailInfoOfDevice(deviceInfo);

        $('.infobox-close').click(function (e) {
            hideDetailInfoOfDevice();
        });
        
    }

    //TODO: get one device info from cloud
    var getDeviceInfo = function (e) {
        var deviceId = e.target.getId();
        //get deviceInfo from deviceId
        var deviceInfo = getMockDeviceInfo(e)
        return deviceInfo;
    }

    var displayInfobox = function (e, device) {
        // Create the infobox for the pushpin
        if (self.pinInfobox != null) {
            hideInfobox(null);
        }

        var buildingDesc = "Building: " + device.building + "<br/>";
        var systemDesc = "System name: " + escapeHtml(device.systemName) + "<br/>";
        var zoneDesc = "Zone: " + device.zone + "<br/>";
        var description = buildingDesc + systemDesc + zoneDesc;

       // var id = e.target.getId();
       // var width = (id.length * 7) + 35;
       // var horizOffset = -(width / 2);

        var infoboxOption = {
            typeName: Microsoft.Maps.InfoboxType.Standard,
            width: 250,
            height: 80,
            visible: true,
            //offset (width, heigh) between infobox and the location point on map
            offset: new Microsoft.Maps.Point(0, 5),

            // Content of infoBox on Map
            title: "",
            description: description
        }
        self.pinInfobox = new Microsoft.Maps.Infobox(e.target.getLocation(),infoboxOption);
        self.map.entities.push(self.pinInfobox);
    }

    var displayDetailInfoOfDevice = function (device) {
        //html because trademark ™
        $('.system-name').html(escapeHtml(device.systemName));
        $('.device-id').text(device.id);
        $('.hub-enable-state').text(device.hubEnableState);
        $('.created-time').text(device.createdTime);
        $('.device-state').text(device.deviceState);
        $('.updated-time').text(device.updatedTime);
        $('.manufacturer').text(device.manufacturer);
        $('.model-number').text(device.modelNumber);
        $('.serial-number').text(device.serialNumber);
        $('.firmware-version').text(device.firmwareVersion);
        $('.platform').text(device.platform);
        $('.processor').text(device.processor);
        $('.installed-ram').text(device.installedRAM);
        $('.latitude').text(device.latitude);
        $('.longtitude').text(device.longitude);
        $('.corperation').text(device.corperation);
        $('.franchise').text(device.franchise);
        $('.siteAddress').text(device.siteAddress);
        $('.systemAddress').text(device.systemAddress);
        $('.building').text(device.building);
        $('.zone').text(device.zone);
        $('.temperature').text(device.temperature);
        $('.humility').text(device.humility);
    }

    var hideInfobox = function (e) {
        if (self.pinInfobox != null) {
            self.pinInfobox.setOptions({ visible: false });
            self.map.entities.remove(self.pinInfobox);
            self.pinInfobox = null;
        }
        hideDetailInfoOfDevice();
    }
    var hideDetailInfoOfDevice = function () {
        $('.system-name').html("");
        $('.device-id').text("");
        $('.hub-enable-state').text("");
        $('.created-time').text("");
        $('.device-state').text("");
        $('.updated-time').text("");
        $('.manufacturer').text("");
        $('.model-number').text("");
        $('.serial-number').text("");
        $('.firmware-version').text("");
        $('.platform').text("");
        $('.processor').text("");
        $('.installed-ram').text("");
        $('.latitude').text("");
        $('.longtitude').text("");
        $('.corperation').text("");
        $('.franchise').text("");
        $('.siteAddress').text("");
        $('.systemAddress').text("");
        $('.building').text("");
        $('.zone').text("");
        $('.temperature').text("");
        $('.humility').text("");
    }
    var escapeHtml =  function escapeHtml(unsafe) {
        return unsafe
             .replace(/&/g, "&amp;")
             .replace(/</g, "&lt;")
             .replace(/>/g, "&gt;")
             .replace(/"/g, "&quot;")
             .replace(/'/g, "&#039;")
             .replace(/™/g, "&trade;");
    }



    return {
        init: init,
        setDeviceLocationData: setDeviceLocationData
    }
}), [jQuery, resources]);

$(function () {
    "use strict";

    IoTApp.MapPane.init();
});