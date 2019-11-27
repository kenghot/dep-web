function SaveAttachmentToDB(projID, removeFiles, addFiles, tableName, fieldName) {
    var url = window.location.protocol + '//' + window.location.host + '/AttachmentHandler/';
    var r = removeFiles.value == "" || removeFiles.value.lengtn == 0 ? null : removeFiles.value;
    var a = addFiles.value == "" || addFiles.value.length == 0 ? null : addFiles.value;
   var p = { "projID": projID, "removeFiles": JSON.parse(r), "addFiles": JSON.parse(a),
   "tableName":tableName, "fieldName": fieldName};
   console.log(p);
          
   axios.post(url + 'savetodb', p)
  .then(response => {
      return response.data;

  });
}