import type { CommissionResult, InterestResult, StockMovementResult } from '../types';
import type { ApiResult, OperationId } from '../types/operations';
import { CommissionResultView } from './CommissionResultView';
import { InterestResultView } from './InterestResultView';
import { StockResultView } from './StockResultView';

interface ResultPanelProps {
	operation: OperationId;
	result: ApiResult | null;
	error: string;
	isLoading: boolean;
}

export function ResultPanel({ operation, result, error, isLoading }: ResultPanelProps) {
	return (
		<section className="result-pane" aria-live="polite">
			<div className="result-heading">
				<div><p className="eyebrow">RESPOSTA</p><h2>Retorno da API</h2></div>
				<span className={`response-status${error ? ' status-error' : result ? ' status-success' : ''}`}>
					<span />{error ? 'Erro' : result ? 'Concluído' : isLoading ? 'Aguardando' : 'Sem resposta'}
				</span>
			</div>
			{isLoading ? (
				<div className="result-empty"><span className="loading-indicator" /><strong>Enviando requisição</strong><span>Aguardando resposta do servidor.</span></div>
			) : error ? (
				<div className="result-message error-message"><span className="message-icon" aria-hidden="true">!</span><div><strong>Falha na chamada</strong><p>{error}</p><small>Verifique a conexão com a API e tente novamente.</small></div></div>
			) : !result ? (
				<div className="result-empty"><span className="empty-mark" aria-hidden="true">↗</span><strong>A resposta aparecerá aqui</strong><span>Preencha os campos e envie a requisição.</span></div>
			) : operation === 'interest' ? (
				<InterestResultView result={result as InterestResult} />
			) : operation === 'stock' ? (
				<StockResultView result={result as StockMovementResult} />
			) : (
				<CommissionResultView result={result as CommissionResult[]} />
			)}
		</section>
	);
}