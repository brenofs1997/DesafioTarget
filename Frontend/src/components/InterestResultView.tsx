import type { InterestResult } from '../types';
import { formatCurrency } from '../utils/formatters';

export function InterestResultView({ result }: { result: InterestResult }) {
	return (
		<div className="result-content">
			<div className="primary-metric"><span>Valor atualizado</span><strong>{formatCurrency(result.value)}</strong></div>
			<dl className="result-rows">
				<div><dt>Juros calculados</dt><dd>{formatCurrency(result.interestAmount)}</dd></div>
				<div><dt>Dias em atraso</dt><dd>{result.daysOverdue}</dd></div>
			</dl>
		</div>
	);
}