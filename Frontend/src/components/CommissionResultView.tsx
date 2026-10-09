import type { CommissionResult } from '../types';
import { formatCurrency } from '../utils/formatters';

export function CommissionResultView({ result }: { result: CommissionResult[] }) {
	if (result.length === 0) {
		return <div className="result-empty compact-empty"><strong>Nenhum resultado</strong><span>A chamada foi concluída sem itens para exibir.</span></div>;
	}

	return (
		<div className="commission-results">
			<div className="result-count">{result.length} {result.length === 1 ? 'resultado' : 'resultados'}</div>
			<div className="table-scroll">
				<table>
					<thead><tr><th>Vendedor</th><th>Vendas</th><th>Comissão</th></tr></thead>
					<tbody>{result.map((item, index) => (
						<tr key={`${item.seller}-${index}`}>
							<td>{item.seller}</td>
							<td>{formatCurrency(item.saleAmount)}</td>
							<td className="commission-value">{formatCurrency(item.commission)}</td>
						</tr>
					))}</tbody>
				</table>
			</div>
		</div>
	);
}