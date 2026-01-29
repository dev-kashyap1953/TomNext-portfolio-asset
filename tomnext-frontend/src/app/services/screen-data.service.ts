import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ScreenItem, FilterOptions } from '../models/screen-data.model';

@Injectable({
  providedIn: 'root'
})
export class ScreenDataService {
  private items: ScreenItem[] = [
    {
      id: '1',
      title: 'Project Alpha',
      description: 'Main development project for Q1',
      category: 'Development',
      status: 'active',
      priority: 'high',
      createdAt: new Date('2024-01-15'),
      updatedAt: new Date('2024-01-20'),
      tags: ['frontend', 'angular', 'typescript'],
      imageUrl: 'https://picsum.photos/seed/project-alpha/400/300'
    },
    {
      id: '2',
      title: 'Marketing Campaign',
      description: 'Q1 marketing strategy and execution',
      category: 'Marketing',
      status: 'pending',
      priority: 'medium',
      createdAt: new Date('2024-01-10'),
      updatedAt: new Date('2024-01-18'),
      tags: ['marketing', 'strategy', 'campaign'],
      imageUrl: 'https://picsum.photos/seed/marketing-campaign/400/300'
    },
    {
      id: '3',
      title: 'Database Migration',
      description: 'Migrate legacy database to new system',
      category: 'Infrastructure',
      status: 'inactive',
      priority: 'high',
      createdAt: new Date('2024-01-05'),
      updatedAt: new Date('2024-01-25'),
      tags: ['database', 'migration', 'infrastructure'],
      imageUrl: 'https://picsum.photos/seed/database-migration/400/300'
    },
    {
      id: '4',
      title: 'UI/UX Redesign',
      description: 'Complete redesign of user interface',
      category: 'Design',
      status: 'active',
      priority: 'medium',
      createdAt: new Date('2024-01-12'),
      updatedAt: new Date('2024-01-22'),
      tags: ['design', 'ui', 'ux'],
      imageUrl: 'https://picsum.photos/seed/ui-redesign/400/300'
    },
    {
      id: '5',
      title: 'API Integration',
      description: 'Integrate third-party payment gateway',
      category: 'Development',
      status: 'pending',
      priority: 'high',
      createdAt: new Date('2024-01-08'),
      updatedAt: new Date('2024-01-19'),
      tags: ['api', 'integration', 'payment'],
      imageUrl: 'https://picsum.photos/seed/api-integration/400/300'
    },
    {
      id: '6',
      title: 'Security Audit',
      description: 'Comprehensive security assessment',
      category: 'Security',
      status: 'active',
      priority: 'high',
      createdAt: new Date('2024-01-03'),
      updatedAt: new Date('2024-01-24'),
      tags: ['security', 'audit', 'compliance'],
      imageUrl: 'https://picsum.photos/seed/security-audit/400/300'
    }
  ];

  private itemsSubject = new BehaviorSubject<ScreenItem[]>(this.items);
  private filterSubject = new BehaviorSubject<FilterOptions>({});

  constructor() {}

  getItems(): Observable<ScreenItem[]> {
    return this.itemsSubject.asObservable();
  }
}