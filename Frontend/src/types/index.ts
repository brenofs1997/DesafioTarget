export interface InterestResult {
	value: number;
	interestAmount: number;
	daysOverdue: number;
}

export interface CommissionResult {
	seller: string;
	saleAmount: number;
	commission: number;
}

export interface StockMovementResult {
	movementId: string;
	productCode: number;
	productDescription: string;
	movementType: string | number;
	description: string;
	quantity: number;
	finalStock: number;
}

export interface InterestRequest {
	value: number;
	dueDate: string;
}

export interface SaleItemRequest {
	vendedor: string;
	valor: number;
}

export interface SaleRequest {
	vendas: SaleItemRequest[];
}

export type StockMovementType = 'Entry' | 'Exit';

export interface StockMovementRequest {
	productCode: number;
	quantity: number;
	type: StockMovementType;
	description: string;
}
