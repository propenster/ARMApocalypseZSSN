# ZSSN (Zombie Survival Social Network).
This is the official repository of the ZSSN API. The ARM8622 Apocalypse Archival System.

### What is ZSSN?
The world as we know it has fallen into an apocalyptic scenario. A laboratory-made virus is transforming human beings and animals into zombies, hungry for fresh flesh. You, as a zombie resistance member (and the last survivor who knows how to code), was designated to develop a system to share resources between non-infected humans.

### Where can I get it?

First, clone this repository [ZSSN](git@github.com:propenster/ARMApocalypseZSSN.git) from the package manager console or git bash by running the command below in your terminal:

```
PM> git clone git@github.com:propenster/ARMApocalypseZSSN.git
```
OR

```
git clone https://github.com/propenster/ARMApocalypseZSSN.git
```
Make sure to have .NET 6 SDK installed on your machine and Visual Studio.


### How do I get started?

After you have cloned the repo, run the code. It's bootstrapped with an InMemoryDB. 

## API Reference

```
BaseUrl: https://localhost:5001
```

### GetAllItems 
Use this endpoint to get all the tradeItems in the Community/Eutopia which include 4 of them according to specification - Water, Food, Medication, Ammunition
```
METHOD: HTTPGET
ResourceURL: /api/v1/core/items/all

RESPONSE: 

{
  "data": [
    {
      "itemId": "string",
      "name": "string",
      "price": 0,
      "isActive": true,
      "isDeleted": true,
      "createdAt": "2022-12-30T08:50:04.062Z",
      "updatedAt": "2022-12-30T08:50:04.062Z"
    }
  ],
  "message": "string",
  "success": true,
  "statusCode": 100
}

```

### RegisterSurvivor 
Use this endpoint to register a new survivor. You must also define the base personal properties (tradeItems) of the survivor to be registered.
```
METHOD: HTTPPOST
ResourceURL: /api/v1/core/survivors/register

REQUEST:
{
  "name": "string",
  "age": 0,
  "gender": "s",
  "lastLocationLongitude": 0,
  "lastLocationLatitude": 0,
  "ownTradeItems": [
    {
      "itemId": "string",
      "survivorId": "string",
      "quantity": 0,
      "unitPoints": 0
    }
  ]
}

RESPONSE: 

{
  "data": {
    "isInfected": true,
    "survivorId": "string",
    "name": "string",
    "age": 0,
    "gender": "s",
    "lastLocationLongitude": 0,
    "lastLocationLatitude": 0,
    "ownTradeItems": [
      {
        "tradeItemId": "string",
        "item": {
          "itemId": "string",
          "name": "string",
          "price": 0,
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:50:26.722Z",
          "updatedAt": "2022-12-30T08:50:26.722Z"
        },
        "survivor": {
          "isInfected": true,
          "survivorId": "string",
          "name": "string",
          "age": 0,
          "gender": "s",
          "lastLocationLongitude": 0,
          "lastLocationLatitude": 0,
          "ownTradeItems": [
            {
              "tradeItemId": "string",
              "item": {
                "itemId": "string",
                "name": "string",
                "price": 0,
                "isActive": true,
                "isDeleted": true,
                "createdAt": "2022-12-30T08:50:26.722Z",
                "updatedAt": "2022-12-30T08:50:26.722Z"
              },
              "survivor": "string",
              "quantity": 0,
              "unitPoints": 0,
              "isActive": true,
              "isDeleted": true,
              "createdAt": "2022-12-30T08:50:26.722Z",
              "updatedAt": "2022-12-30T08:50:26.722Z"
            }
          ],
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:50:26.722Z",
          "updatedAt": "2022-12-30T08:50:26.722Z"
        },
        "quantity": 0,
        "unitPoints": 0,
        "isActive": true,
        "isDeleted": true,
        "createdAt": "2022-12-30T08:50:26.722Z",
        "updatedAt": "2022-12-30T08:50:26.722Z"
      }
    ],
    "isActive": true,
    "isDeleted": true,
    "createdAt": "2022-12-30T08:50:26.722Z",
    "updatedAt": "2022-12-30T08:50:26.722Z"
  },
  "message": "string",
  "success": true,
  "statusCode": 100
}

```


### UpdateSurvivorLocation 
Use this endpoint to update survivor's last known location in longitude and latitude
```
METHOD: HTTPPOST
ResourceURL: /api/v1/core/survivors/location/update

REQUEST:
{
  "survivorId": "string",
  "lastLocationLongitude": 0,
  "lastLocationLatitude": 0,
}

RESPONSE: 

{
  "data": {
    "isInfected": true,
    "survivorId": "string",
    "name": "string",
    "age": 0,
    "gender": "s",
    "lastLocationLongitude": 0,
    "lastLocationLatitude": 0,
    "ownTradeItems": [
      {
        "tradeItemId": "string",
        "item": {
          "itemId": "string",
          "name": "string",
          "price": 0,
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:50:26.722Z",
          "updatedAt": "2022-12-30T08:50:26.722Z"
        },
        "survivor": {
          "isInfected": true,
          "survivorId": "string",
          "name": "string",
          "age": 0,
          "gender": "s",
          "lastLocationLongitude": 0,
          "lastLocationLatitude": 0,
          "ownTradeItems": [
            {
              "tradeItemId": "string",
              "item": {
                "itemId": "string",
                "name": "string",
                "price": 0,
                "isActive": true,
                "isDeleted": true,
                "createdAt": "2022-12-30T08:50:26.722Z",
                "updatedAt": "2022-12-30T08:50:26.722Z"
              },
              "survivor": "string",
              "quantity": 0,
              "unitPoints": 0,
              "isActive": true,
              "isDeleted": true,
              "createdAt": "2022-12-30T08:50:26.722Z",
              "updatedAt": "2022-12-30T08:50:26.722Z"
            }
          ],
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:50:26.722Z",
          "updatedAt": "2022-12-30T08:50:26.722Z"
        },
        "quantity": 0,
        "unitPoints": 0,
        "isActive": true,
        "isDeleted": true,
        "createdAt": "2022-12-30T08:50:26.722Z",
        "updatedAt": "2022-12-30T08:50:26.722Z"
      }
    ],
    "isActive": true,
    "isDeleted": true,
    "createdAt": "2022-12-30T08:50:26.722Z",
    "updatedAt": "2022-12-30T08:50:26.722Z"
  },
  "message": "string",
  "success": true,
  "statusCode": 100
}

```

### UpdateSurvivorInfection 
Use this endpoint to update survivor's infection status. In other words, use this to set a survivor as being infected or not.
```
METHOD: HTTPPOST
ResourceURL: /api/v1/core/survivors/infectionstatus/update

REQUEST:
{
  "survivorId": "string",
  "lastLocationLongitude": 0,
  "lastLocationLatitude": 0,
}

RESPONSE: 

{
  "data": {
    "isInfected": true,
    "survivorId": "string",
    "name": "string",
    "age": 0,
    "gender": "s",
    "lastLocationLongitude": 0,
    "lastLocationLatitude": 0,
    "ownTradeItems": [
      {
        "tradeItemId": "string",
        "item": {
          "itemId": "string",
          "name": "string",
          "price": 0,
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:50:26.722Z",
          "updatedAt": "2022-12-30T08:50:26.722Z"
        },
        "survivor": {
          "isInfected": true,
          "survivorId": "string",
          "name": "string",
          "age": 0,
          "gender": "s",
          "lastLocationLongitude": 0,
          "lastLocationLatitude": 0,
          "ownTradeItems": [
            {
              "tradeItemId": "string",
              "item": {
                "itemId": "string",
                "name": "string",
                "price": 0,
                "isActive": true,
                "isDeleted": true,
                "createdAt": "2022-12-30T08:50:26.722Z",
                "updatedAt": "2022-12-30T08:50:26.722Z"
              },
              "survivor": "string",
              "quantity": 0,
              "unitPoints": 0,
              "isActive": true,
              "isDeleted": true,
              "createdAt": "2022-12-30T08:50:26.722Z",
              "updatedAt": "2022-12-30T08:50:26.722Z"
            }
          ],
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:50:26.722Z",
          "updatedAt": "2022-12-30T08:50:26.722Z"
        },
        "quantity": 0,
        "unitPoints": 0,
        "isActive": true,
        "isDeleted": true,
        "createdAt": "2022-12-30T08:50:26.722Z",
        "updatedAt": "2022-12-30T08:50:26.722Z"
      }
    ],
    "isActive": true,
    "isDeleted": true,
    "createdAt": "2022-12-30T08:50:26.722Z",
    "updatedAt": "2022-12-30T08:50:26.722Z"
  },
  "message": "string",
  "success": true,
  "statusCode": 100
}

```


### FlagSurvivorAsInfected 
Use this endpoint to flag a particular survivor as being infected.
```
METHOD: HTTPPOST
ResourceURL: /api/v1/core/survivors/infectionstatus/flag

REQUEST:
{
  "survivorId": "string",
  "isInfected": true
}

RESPONSE: 

{
  "data": {
    "isInfected": true,
    "survivorId": "string",
    "name": "string",
    "age": 0,
    "gender": "s",
    "lastLocationLongitude": 0,
    "lastLocationLatitude": 0,
    "ownTradeItems": [
      {
        "tradeItemId": "string",
        "item": {
          "itemId": "string",
          "name": "string",
          "price": 0,
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:55:26.744Z",
          "updatedAt": "2022-12-30T08:55:26.744Z"
        },
        "survivor": {
          "isInfected": true,
          "survivorId": "string",
          "name": "string",
          "age": 0,
          "gender": "s",
          "lastLocationLongitude": 0,
          "lastLocationLatitude": 0,
          "ownTradeItems": [
            {
              "tradeItemId": "string",
              "item": {
                "itemId": "string",
                "name": "string",
                "price": 0,
                "isActive": true,
                "isDeleted": true,
                "createdAt": "2022-12-30T08:55:26.744Z",
                "updatedAt": "2022-12-30T08:55:26.744Z"
              },
              "survivor": "string",
              "quantity": 0,
              "unitPoints": 0,
              "isActive": true,
              "isDeleted": true,
              "createdAt": "2022-12-30T08:55:26.744Z",
              "updatedAt": "2022-12-30T08:55:26.744Z"
            }
          ],
          "isActive": true,
          "isDeleted": true,
          "createdAt": "2022-12-30T08:55:26.744Z",
          "updatedAt": "2022-12-30T08:55:26.744Z"
        },
        "quantity": 0,
        "unitPoints": 0,
        "isActive": true,
        "isDeleted": true,
        "createdAt": "2022-12-30T08:55:26.744Z",
        "updatedAt": "2022-12-30T08:55:26.744Z"
      }
    ],
    "isActive": true,
    "isDeleted": true,
    "createdAt": "2022-12-30T08:55:26.744Z",
    "updatedAt": "2022-12-30T08:55:26.744Z"
  },
  "message": "string",
  "success": true,
  "statusCode": 100
}

```


### CreateTrade 
Use this endpoint to create trade between two survivors
```
METHOD: HTTPPOST
ResourceURL: /api/v1/core/trade/create

REQUEST:
{
  "buyerSurvivorId": "string",
  "sellerSurvivorId": "string",
  "buyerItems": [
    {
      "itemId": "string",
      "survivorId": "string",
      "quantity": 0,
      "unitPoints": 0
    }
  ],
  "sellerItems": [
    {
      "itemId": "string",
      "survivorId": "string",
      "quantity": 0,
      "unitPoints": 0
    }
  ]
}

RESPONSE: 

{
  "data": "string",
  "message": "string",
  "success": true,
  "statusCode": 100
}

```





### FetchReports 
Use this endpoint to fetch reports
```
METHOD: HTTPGET
ResourceURL: /api/v1/core/reports/all

RESPONSE: 

{
  "data": {
    "infectedSurvivorsPercent": 0,
    "nonInfectedSurvivorsPercent": 0,
    "itemAverageResponses": [
      {
        "itemName": "string",
        "average": 0,
        "total": 0
      }
    ],
    "pointsLostDueToInfection": 0
  },
  "message": "string",
  "success": true,
  "statusCode": 100
}

```

## API Security
The OpenAPI specification document for this service was run through 42Crunch to validate against attack vectors and potential vulnerabilitys in the OWASP top 10 list.
See the image of the report below...


## 

