# TrainStop Revisited ~ C# Project

Looking at the TrainStop Project again. This time; implementing dependency injection for simpler testing.
Also, using Shouldly for more fluent assertions. For testing; using xUnit. 


User Stories;

Trains have names [✓]
```
As a Train Manager,
So I can easily refer to my train,
Trains should have names.
```

Stations have names [ ]
```
As a Train Manager,
So I can easily refer to my destinations,
Stations should have names.
```

Trains have a state of motion [ ]
```
As a Train Manager,
So I know when a train has left,
Trains should have a state of motion.
```

Stations should be able to hold trains [ ]
```
As a Train Manager,
So I can stop my train at a station,
Stations should be able to hold a train.
```

Train's state of motion can be set to true [ ]
```
As a Train Manager,
So I can start my train,
Trains should be able to be set in motion.
```

Trains's state of motion can be set to false [ ]
```
As a Train Manager,
So I can stop my train,
Trains should be able to be set as stopped.
```

Stations should be able to receive trains [ ]
```
As a Train Manager,
So I can enter a station by train,
Stations should have an effective method by which to receive trains.
```

Stations can only receive trains on a journey [ ]
```
As a Station Manager,
To make sure trains park within the station,
Stations should only take Trains on a journey.
```

Trains received by Stations should have their journies stopped [ ]
```
As a Train Manager,
So passengers can easily dissembark,
Trains should be stopped at Stations.
```

Trains can depart from Stations [ ]
```
As a Train Manager,
So I can leave a station by train,
Stations should have an effective method by which to release trains.
```

Exception thrown when a train which doesn't exist is released from station [ ]
```
As a Station Manager,
So I know when a train release was faulty,
An Alert should be raised when a train is released which isn't at the station.
```

Trains which depart from Stations should begin their journey [ ]
```
As a Train Manager,
So I can make it on time to my destinations,
Train journies should be started when leaving a station.
```

Stations can only release stopped trains [ ]
```
As a Station Manager,
To make sure trains slowly leave the station,
Stations should only release Trains which are stopped.
```

Stations are instantiated with a maximum capacity for trains [ ]
```
As a Station Manager,
So I can avoid needless work,
Not passing a capacity will set a default capacity value.
```

Stations can be instantiated with a maximum capacity passed for trains [ ]
```
As a Station Manager,
So I can manage expected traffic of trains,
Stations should be instantiated with a passed capacity.
```

Stations can store a maximum number of trains [ ]
```
As a Station Manager,
So I avoid trains crashing,
Stations should not receive a train when at capacity.
```

Stations can be set have maintenance on them [ ]
```
As a Station Manager,
So my station can grow and repair,
Maintenance should be allowed to start on a station.
```

Stations under maintenance cannot receive or release any trains [ ]
```
As a Station Manager,
To prevent any accidents,
Stations should not receive or release a train when under maintenance.
```

Maintenance on stations can end [ ]
```
As a Station Manager,
So my station can be accessible again to trains,
Maintenance should be allowed to stop on a station.
```