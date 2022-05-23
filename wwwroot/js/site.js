window.addEventListener('DOMContentLoaded', (event) => {

    let elementDivBizagiOdataServices = document.getElementById("divBizagiOdataServices");

    elementDivBizagiOdataServices.addEventListener("click", (e) => {

        if (e.target.id && e.target.classList.contains("btn-bizagi")) {
            document.getElementById("txtUrl").value = e.target.value;                  
        }

    }); 

    let btnExecuteApi = document.getElementById("btnExecuteApi");
    let btnNext = document.getElementById("btnNext");
    let btnNew = document.getElementById("btnNew");

    btnExecuteApi.addEventListener("click", (e) => {
        bindRequestDataAndPost("execute","GET");
    });

    btnNext.addEventListener("click", (e) => {
        bindRequestDataAndPost("next","POST");
    });

    btnNew.addEventListener("click", (e) => {
        bindRequestDataAndPost("new", "POST");
    });

    
});

 async function bindRequestDataAndPost(requestType,requestMethod) {

     let bizagiRequest = {}; 
     
        bizagiRequest["requestMethod"] = requestMethod;
        bizagiRequest["requestUrl"] = document.getElementById("txtUrl").value;
        bizagiRequest["processId"] = document.getElementById("txtProcessId").value;
        bizagiRequest["caseId"] = document.getElementById("txtCaseId").value;
        bizagiRequest["workItemId"] = document.getElementById("txtWorkItemId").value; 
        bizagiRequest["requestType"] = requestType;

    if (requestType == 'next') {
        bizagiRequest["postBody"] = document.getElementById("txtJsonBody").value;   
    }

    await postData(bizagiRequest);
   
}

 async function postData(bizagiRequest) {

    let url = "/api/bizagi";
    await postRequest(url, bizagiRequest);   
      
}

async function postRequest(url = '', data = {}) {

    fetch(url, fnBindPostHeaderRequest(data))
        .then(function (response) {
            if (!response.ok) {

                document.getElementById("txtJsonResult").value = response.status;   
                
            }
            return response;
       }).then(function (response) {

            document.getElementById("txtJsonResult").value = JSON.stringify(response);

       }).catch(function (error) {
            console.log(error);
            document.getElementById("txtJsonResult").value = error;
     });

}

 function fnBindPostHeaderRequest(data = {}) {
    
    let requestParams = {

        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit

        headers: {
            'Content-Type': 'application/json'            
        },

        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(data) // body data type must match "Content-Type" header

    }

    return requestParams;

}
    
   