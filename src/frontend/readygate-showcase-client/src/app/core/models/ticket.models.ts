export const ticketStatuses = ['Open', 'Pending', 'Resolved'] as const;
export const ticketPriorities = ['Low', 'Medium', 'High', 'Critical'] as const;

export type TicketStatus = (typeof ticketStatuses)[number];
export type TicketPriority = (typeof ticketPriorities)[number];

export interface TicketSummary {
  readonly id: number;
  readonly reference: string;
  readonly subject: string;
  readonly customerName: string;
  readonly status: TicketStatus;
  readonly priority: TicketPriority;
  readonly flagged: boolean;
  readonly updatedAt: string;
}

export interface TicketQuery {
  readonly status?: TicketStatus;
  readonly priority?: TicketPriority;
}

export interface TicketExportRequest {
  readonly ticketIds: readonly number[];
}
