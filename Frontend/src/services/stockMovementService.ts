import type { StockMovementRequest, StockMovementResult } from '../types';
import { api } from './api';

export const stockMovementService = {
	async create(request: StockMovementRequest): Promise<StockMovementResult> {
		const { data } = await api.post<StockMovementResult>(
			'/StockMovements',
			request,
		);
		return data;
	},
};