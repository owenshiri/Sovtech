# Sovtech
This project uses GraphQL to query Chuck Norris endpoint and Swapi endpoint. I am using Playground ui to help documenting the api
Below are the links to the requested endpoints
Chuck - https://localhost:44394/ui/chuck
this one maps to - https://localhost:44394/api/chuck
example query to use 

{
 categories
}


Swapi - https://localhost:44394/ui/swapi
this one maps to - https://localhost:44394/api/swapi

example query
{
  peoples(first:10){
    totalCount
    pageInfo{
      endCursor
      hasNextPage
      hasPreviousPage
    }
    items{
      name
      gender
      mass
    }
    edges{
      node{
        name
        gender
      }
    }
    }
}


Search - https://localhost:44394/ui/search
this one maps to https://localhost:44394/api/search
Example query
{
  jokes(value:"chuck"){
    id
    categories
  }
  people(value:"luke"){
    name
  }
}
