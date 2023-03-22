# file-uploader

## Introduction

This is a simple file storage system using the following technology stack:

.Net Core 6
SQLite
Enttity Framework

A sample user is included in the Database with the following credentials:
`Username`: `TestUser`
`Password`: `TestPassword`

Swagger documentation is avaialle upon build at `/swagger`.  It is a fairly simple REST-ful API with the following endpoints:

`GET`: `/api/FileUploader` - Test API that lists all Users in the `Users` table
`POST` : `/api/FileUploader` - Allows an **authorized** user to `Upload` a number of files.  Any files with the same filename will be marked as the most recent version and the previous version will be versioned with the next highest version number for that file.
`GET`: `/api/FileUploader/{UserId}` - Allows a search of files for a specific user.  Searh parameters can be by `FileName` or `Version` number
`GET`: `/api/FileUploader/Download/{UserId}` - Allows the download of a file by either a `FileId` or `FileName`.  A `FileId` is specific whereas a `FileName` will download the most recent version.
`DELETE`: `/api/FileUploader/{FileId}` - Allows an **authorized** user to `Delete` their own file

## Endpoint Instructions

### `GET`: `/api/FileUploader` 

Sample Response
```json
[
  {
    "id": 1,
    "userName": "TestUser",
    "password": "TestPassword"
  }
]
```

### `POST` : `/api/FileUploader` 

The following fields in a request `Body` are necessary to `POST` to this endpoint:
`userName` - string
`password` - string
`userFiles` - an array of Files

Sample Response
```json
[
  {
    "contentType": "image/jpeg",
    "contentDisposition": "form-data; name=\"userFiles\"; filename=\"20190715_211203.jpeg\"",
    "headers": {
      "Content-Disposition": [
        "form-data; name=\"userFiles\"; filename=\"20190715_211203.jpeg\""
      ],
      "Content-Type": [
        "image/jpeg"
      ]
    },
    "length": 182482,
    "name": "userFiles",
    "fileName": "20190715_211203.jpeg"
  },
  {
    "contentType": "image/png",
    "contentDisposition": "form-data; name=\"userFiles\"; filename=\"Capture.PNG\"",
    "headers": {
      "Content-Disposition": [
        "form-data; name=\"userFiles\"; filename=\"Capture.PNG\""
      ],
      "Content-Type": [
        "image/png"
      ]
    },
    "length": 68982,
    "name": "userFiles",
    "fileName": "Capture.PNG"
  }
]
```

### `GET`: `/api/FileUploader/{UserId}`

```json
[
  {
    "id": 88,
    "userId": 1,
    "fileName": "20190715_211203",
    "fileExtension": ".jpeg",
    "fileLocation": "TestUser",
    "version": 2
  },
  {
    "id": 89,
    "userId": 1,
    "fileName": "Capture",
    "fileExtension": ".PNG",
    "fileLocation": "TestUser",
    "version": 2
  },
  {
    "id": 90,
    "userId": 1,
    "fileName": "20190715_211203",
    "fileExtension": ".jpeg",
    "fileLocation": "TestUser",
    "version": 0
  },
  {
    "id": 91,
    "userId": 1,
    "fileName": "Capture",
    "fileExtension": ".PNG",
    "fileLocation": "TestUser",
    "version": 0
  },
  {
    "id": 92,
    "userId": 1,
    "fileName": "20190715_211203",
    "fileExtension": ".jpeg",
    "fileLocation": "TestUser",
    "version": 0
  },
  {
    "id": 93,
    "userId": 1,
    "fileName": "Capture",
    "fileExtension": ".PNG",
    "fileLocation": "TestUser",
    "version": 0
  }
]
```
 
`GET`: `/api/FileUploader/Download/{UserId}` 

Sample Request: [https://localhost:7132/api/FileUploader/Download/1?FileName=Capture&FileId=0&Version=0]https://localhost:7132/api/FileUploader/Download/1?FileName=Capture&FileId=0&Version=0

`DELETE`: `/api/FileUploader/{FileId}`