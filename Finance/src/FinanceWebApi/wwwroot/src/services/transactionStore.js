angular.module("finance").factory("TransactionStore", function($http){
	var _getAll = function(propertyId){
		return $http.get("http://localhost:5000/api/transaction/"+propertyId);
	}
					
	var _save = function(item){
	 	return $http.post("http://localhost:5000/api/transaction/", item);
	}
					
	return {
		getAll: _getAll,
		save: _save
	}
});