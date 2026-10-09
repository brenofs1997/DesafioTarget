import type { InterestRequest, InterestResult } from '../types';
import { api } from './api';

export const interestService = {
	async calculate(request: InterestRequest): Promise<InterestResult> {
		const { data } = await api.get<InterestResult>('/Interest/calculate', {
			params: request,
		});

		return data;
	},
};