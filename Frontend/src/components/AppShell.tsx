import { useState } from 'react';
import { commissionService } from '../services/commissionService';
import { interestService } from '../services/interestService';
import { stockMovementService } from '../services/stockMovementService';
import { OPERATIONS, type ApiResult, type OperationId } from '../types/operations';
import { getErrorMessage } from '../utils/getErrorMessage';
import { CommissionForm, CommissionQueryForm } from './CommissionForms';
import { InterestForm } from './InterestForm';
import { PageHeader } from './PageHeader';
import { ResultPanel } from './ResultPanel';
import { Sidebar } from './Sidebar';
import { StockForm } from './StockForm';
import '../App.css';

export function AppShell() {
	const [activeOperation, setActiveOperation] = useState<OperationId>('interest');
	const [result, setResult] = useState<ApiResult | null>(null);
	const [error, setError] = useState('');
	const [isLoading, setIsLoading] = useState(false);

	async function execute(request: () => Promise<ApiResult>) {
		setIsLoading(true);
		setError('');
		setResult(null);

		try {
			setResult(await request());
		} catch (requestError) {
			setError(getErrorMessage(requestError));
		} finally {
			setIsLoading(false);
		}
	}

	function selectOperation(operation: OperationId) {
		setActiveOperation(operation);
		setResult(null);
		setError('');
	}

	return (
		<div className="app-shell">
			<Sidebar activeOperation={activeOperation} onSelect={selectOperation} />
			<main className="main-area" id="inicio">
				<PageHeader operation={OPERATIONS[activeOperation]} />
				<section className="workbench" aria-label="Executar chamada">
					<div className="form-pane">
						{activeOperation === 'interest' && (
							<InterestForm
								isLoading={isLoading}
								onSubmit={(request) => void execute(() => interestService.calculate(request))}
							/>
						)}
						{activeOperation === 'commission-query' && (
							<CommissionQueryForm
								isLoading={isLoading}
								onSubmit={() => void execute(() => commissionService.calculate())}
							/>
						)}
						{activeOperation === 'commission-submit' && (
							<CommissionForm
								isLoading={isLoading}
								onSubmit={(request) => void execute(() => commissionService.calculateFromSales(request))}
							/>
						)}
						{activeOperation === 'stock' && (
							<StockForm
								isLoading={isLoading}
								onSubmit={(request) => void execute(() => stockMovementService.create(request))}
							/>
						)}
					</div>
					<ResultPanel operation={activeOperation} result={result} error={error} isLoading={isLoading} />
				</section>
				<footer className="page-footer">
					<span>NEXO <span className="footer-divider">/</span> Console de serviços</span>
					<span>REST · JSON</span>
				</footer>
			</main>
		</div>
	);
}