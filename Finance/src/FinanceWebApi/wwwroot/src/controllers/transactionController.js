angular.module("finance").controller("TransactionController", function(TransactionStore){
	this.newItem = {};	
	this.itens = TransactionStore.getAll(1);
					
	this.addItem = function(newItem){
		TransactionStore.save(newItem);
		this.newItem = {};
	};
});