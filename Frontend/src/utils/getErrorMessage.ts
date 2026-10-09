import { isAxiosError } from 'axios';

export function getErrorMessage(error: unknown): string {
	if (isAxiosError(error)) {
		const data: unknown = error.response?.data;
		if (typeof data === 'string') return data;
		if (data && typeof data === 'object' && 'message' in data && typeof data.message === 'string') {
			return data.message;
		}
		return error.message;
	}

	return error instanceof Error ? error.message : 'Não foi possível concluir a chamada.';
}