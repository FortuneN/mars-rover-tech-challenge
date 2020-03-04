# Mars Rover Tech Challenge

## Background

A squad of robotic rovers are to be landed by NASA on a plateau on Mars.

This plateau, which is curiously rectangular, must be navigated by the rovers so that their on board cameras can get a complete view of the surrounding terrain to send back to Earth.

A rover's position is represented by a combination of an x and y co-ordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and 'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot.

'M' means move forward one grid point, and maintain the same heading.

Assume that the square directly North from (x, y) is (x, y+1).

### Input:

The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be 0,0.

The rest of the input is information pertaining to the rovers that have been deployed. Each rover has two lines of input. The first line gives the rover's position, and the second line is a series of instructions telling the rover how to explore the plateau.

The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.

Each rover will be finished sequentially, which means that the second rover won't start to move until the first one has finished moving.

##### Test Input:

5 5

1 2 N

LMLMLMLMM

3 3 E

### Output:

The output for each rover should be its final co-ordinates and heading.

MMRMMRMRRM

##### Expected Output:

1 3 N

5 1 E

## Solution

This solution addresses the challenge specified above in 5 projects:

- MRTC.Library (MRTC.Library.dll)
  
  The core library with the business logic. The library can be included in many types of projects to suite any architecture.
  
- MRTC.Library.Tests

  Unit tests for the library
  
- MRTC.CommandLineInterface (mrtc-cli.exe)

  A command line utility that wraps the core library and presents a command line oriented user interface.
  
  Has no dependencies
  
  Use `mrtc-cli -h` option for help 

- MRTC.TelnetServer (mrtc-telnet-server.exe)

  A telnet server that wraps the core library and presents a telnet protocol user interface
  
  Runs on `localhost:5555`
  
  Connect with telnet client. Telnet client can connect if this is running.

- MRTC.RestApiServer (mrtc-web-api.exe)

  A web server that wraps the core library and presents an HTTP REST oriented application programming interface.
  
  Runs on `http://localhost:7777`
  
- MRTC.WebClient (mrtc-web-client.exe)

  A web server that presents a user friendly graphical user interface which interfaces with the REST API to make use on the features provided in the core library
  
  Runs on `http://localhost:8888`
  
  Use any browser. RestApiServer must be running.
  
## Running the solution

Go to [releases](https://github.com/FortuneN/mars-rover-tech-challenge/releases) and download the latest zip file and extract

The zip file contains the items described above.

You can run each by simply double-clicking.

No authentication is required.

### Requirements
- .Net Framework 4.8 (dotnet-framework-installer-48.exe)
- .Net Core 3.0 (dotnet-runtime-3.0.0-win-x64.exe + aspnetcore-runtime-3.0.0-win-x64.exe + windowsdesktop-runtime-3.0.0-win-x64.exe)
