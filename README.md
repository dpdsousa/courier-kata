# Courier Kata

## About
The main purpose of this test is to create a code library to calculate the cost of sending an order of parcels. The pdf containing the info about this kata can be found on the docs directory.

### Built with
* [Microsoft Visual Studio Community 2019](https://visualstudio.microsoft.com/vs/community/)
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.301-windows-x64-installer)


## Code usage

To calculate the cost of sending and order of parcels the user simply needs to create the desired parcels with the chosen size and weight. To "finalize" the order it's then necessary to create the ParcelOrder.

Create Parcel example:
```cs
    var smallParcel = new Parcel(length, width, heigth, weight);
```

Create list of Parcels example:
```cs
    var severalParcels = new List<Parcel>
    {
        new Parcel(length, width, heigth, weight),
        new Parcel(length, width, heigth, weight),
        new Parcel(length, width, heigth, weight),
        new Parcel(length, width, heigth, weight)
    };
```


Create order of parcels example:
```cs
    var newParcelOrder = new ParcelOrder(singleParcel, speedyShipping);
    
    //OR
    
    var newParcelOrder = new ParcelOrder(severalParcels, speedyShipping);
```

## Run tests
To run the tests it's only necessary to execute the following command on the base folder of the project (the one that contains the .sln file).
```
dotnet test
```


