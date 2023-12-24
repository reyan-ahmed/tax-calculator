
## How to use it

Run the project directly on Visual Studio, 

Here we have a sample which won't apply the single charge rule, as it is stockholm city Id

```bash
{
  "vehicleTypeId": "3878df9a-c2ef-41ff-b62b-2bfcfcf6e5dc",
  "CityId":"0a4fbeff-11bb-416f-a7ea-03d1ed282728",
  "Dates": [
   
    "2013-12-24T18:17:17.786Z",
    "2013-12-24T11:18:17.786Z",
    "2013-12-24T11:18:17.786Z",
    "2013-12-18T17:20:17.786Z",
    "2013-12-18T18:10:17.786Z"
  ]
}
```

To apply The single charge rule. You need to use this body 
    
    
```bash
{
  "vehicleTypeId": "3878df9a-c2ef-41ff-b62b-2bfcfcf6e5dc",
  "CityId":"ec518ee0-81ee-4728-89a0-d0c88d5cfd3b",
  "Dates": [
   
    "2013-12-24T18:17:17.786Z",
    "2013-12-24T11:18:17.786Z",
    "2013-12-24T11:18:17.786Z",
    "2013-12-18T17:20:17.786Z",
    "2013-12-18T18:10:17.786Z"
  ]
}
```

And here is the example for Tax Exempt vehicles

```bash
{
  "vehicleTypeId": "90f20bee-4bb9-47fa-8e44-d2a95aa76f2f",
  "CityId":"ec518ee0-81ee-4728-89a0-d0c88d5cfd3b",
  "Dates": [
   
    "2013-12-24T18:17:17.786Z",
    "2013-12-24T11:18:17.786Z",
    "2013-12-24T11:18:17.786Z",
    "2013-12-18T17:20:17.786Z",
    "2013-12-18T18:10:17.786Z"
  ]
}
```


## Improvement:
There are alot of things left for Improvement, The unit test left, As we should connected with database and get tax details and exempt vehicles details from the database.
Get once only the dates with tax details for the specific city and then call this method to get total tax.
Also can handle properly error messages, as we as validation
