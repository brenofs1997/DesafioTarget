import type { CommissionResult, SaleRequest } from '../types';
import { api } from './api';

export const commissionService = {
	async calculate(): Promise<CommissionResult[]> {
		const { data } = await api.get<CommissionResult[]>('/Commissions/calculate');
		return data;
	},

	async calculateFromSales(request: SaleRequest): Promise<CommissionResult[]> {
		const { data } = await api.post<CommissionResult[]>(
			'/Commissions/calculate',
			request,
		);
		return data;
	},
};