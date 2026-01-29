export interface ScreenItem {
  id: string;
  title: string;
  description: string;
  category: string;
  status: 'active' | 'inactive' | 'pending';
  priority: 'high' | 'medium' | 'low';
  createdAt: Date;
  updatedAt: Date;
  tags: string[];
  imageUrl?: string;
}

export interface FilterOptions {
  category?: string;
  status?: string;
  priority?: string;
  search?: string;
}

export interface LayoutConfig {
  leftPanelWidth: number;
  rightPanelWidth: number;
  isMobile: boolean;
}