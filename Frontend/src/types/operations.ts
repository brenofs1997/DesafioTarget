import type { CommissionResult, InterestResult, StockMovementResult } from './index';

export type OperationId = 'interest' | 'commission-query' | 'commission-submit' | 'stock';
export type ApiResult = InterestResult | CommissionResult[] | StockMovementResult;

export interface OperationDetails {
	group: string;
	label: string;
	title: string;
	method: 'GET' | 'POST';
	route: string;
	intro: string;
}

export const OPERATIONS: Record<OperationId, OperationDetails> = {
	interest: {
		group: 'Financeiro',
		label: 'Calcular juros',
		title: 'Cálculo de juros',
		method: 'GET',
		route: '/Interest/calculate',
		intro: 'Informe o valor original e a data de vencimento.',
	},
	'commission-query': {
		group: 'Comissões',
		label: 'Consultar resultados',
		title: 'Consultar comissões',
		method: 'GET',
		route: '/Commissions/calculate',
		intro: 'Consulte os resultados de comissão disponíveis na API.',
	},
	'commission-submit': {
		group: 'Comissões',
		label: 'Enviar vendas',
		title: 'Calcular por vendas',
		method: 'POST',
		route: '/Commissions/calculate',
		intro: 'Adicione os vendedores e os valores para calcular as comissões.',
	},
	stock: {
		group: 'Estoque',
		label: 'Registrar movimentação',
		title: 'Movimentação de estoque',
		method: 'POST',
		route: '/StockMovements',
		intro: 'Registre uma entrada ou saída para um produto.',
	},
};

export const MENU_GROUPS: { label: string; items: OperationId[] }[] = [
	{ label: 'Financeiro', items: ['interest'] },
	{ label: 'Comissões', items: ['commission-query', 'commission-submit'] },
	{ label: 'Estoque', items: ['stock'] },
];

export const MENU_SYMBOLS: Record<OperationId, string> = {
	interest: 'J',
	'commission-query': 'C',
	'commission-submit': '+',
	stock: 'E',
};