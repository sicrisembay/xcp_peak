# XCP PEAK
This is a PC host C# application based from from Peak [PCAN-XCP](https://www.peak-system.com/PCAN-XCP-API.445.0.html?&L=1) API.  I use this for testing my XCPBasic port ([xcp_c2000](https://github.com/sicrisembay/xcp_c2000)) to C2000 (TMS320F28335) microcontroller.


## Build
This is a C# application.  You need to install Visual Studio.  I used VS2019 at the time of writing this.


## Limitation
List of some limitation:
- Support only PCAN adapters
- Support only word Address Ganularity
- Only tested for Upload/Download and basic DAQ

## Screenshots

Connection with PCAN adapter.
<p align="center">
  <img src="https://github.com/sicrisembay/xcp_peak/blob/main/doc/img/screenshot_connected.png">
</p>


DAQ configuration.
<p align="center">
  <img src="https://github.com/sicrisembay/xcp_peak/blob/main/doc/img/screenshot_daqTest.png">
</p>


Plotting DAQ data.
<p align="center">
  <img src="https://github.com/sicrisembay/xcp_peak/blob/main/doc/img/screenshot_daqTestPlot.png">
</p>