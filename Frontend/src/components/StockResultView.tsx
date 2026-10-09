import type { StockMovementResult } from '../types';

export function StockResultView({ result }: { result: StockMovementResult }) {
	return (
		<div className="result-content">
			<div className="stock-confirmation"><span aria-hidden="true">✓</span><div><strong>Movimentação registrada</strong><small>{result.productDescription}</small></div></div>
			<dl className="result-rows">
				<div><dt>Movimentação</dt><dd><code>{result.movementId}</code></dd></div>
				<div><dt>Produto</dt><dd>{result.productCode}</dd></div>
				<div><dt>Tipo</dt><dd>{String(result.movementType)}</dd></div>
				<div><dt>Quantidade</dt><dd>{result.quantity}</dd></div>
				<div><dt>Estoque final</dt><dd>{result.finalStock}</dd></div>
				<div><dt>Descrição</dt><dd>{result.description}</dd></div>
			</dl>
		</div>
	);
}