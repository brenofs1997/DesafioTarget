import { MENU_GROUPS, MENU_SYMBOLS, OPERATIONS, type OperationId } from '../types/operations';

interface SidebarProps {
	activeOperation: OperationId;
	onSelect: (operation: OperationId) => void;
}

export function Sidebar({ activeOperation, onSelect }: SidebarProps) {
	return (
		<aside className="sidebar">
			<a className="brand" href="#inicio" aria-label="Nexo, início">
				<span className="brand-mark" aria-hidden="true">n</span>
				<span className="brand-copy"><strong>nexo</strong><span>PAINEL DE OPERAÇÕES</span></span>
			</a>
			<nav className="side-navigation" aria-label="Chamadas da API">
				{MENU_GROUPS.map((group) => (
					<div className="nav-group" key={group.label}>
						<p className="nav-group-label">{group.label}</p>
						{group.items.map((operationId) => (
							<button
								className={`nav-item${activeOperation === operationId ? ' is-active' : ''}`}
								key={operationId}
								onClick={() => onSelect(operationId)}
								aria-current={activeOperation === operationId ? 'page' : undefined}
							>
								<span className="nav-symbol" aria-hidden="true">{MENU_SYMBOLS[operationId]}</span>
								<span className="nav-item-label">{OPERATIONS[operationId].label}</span>
							</button>
						))}
					</div>
				))}
			</nav>
			<div className="sidebar-footer">
				<span className="connection-dot" /><span>API local</span><code>8080</code>
			</div>
		</aside>
	);
}