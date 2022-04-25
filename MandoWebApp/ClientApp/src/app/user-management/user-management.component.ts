
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { arrayEquals } from '../utilities/array-util';
import { extractFirstErrorMessage } from '../utilities/error-util';
import { AuthorizeService, getRoles } from '../../api-authorization/authorize.service';
import { Profile } from 'oidc-client';
import { LocalizedMessageService } from '../_services/localized-message.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html'
})
export class UserManagementComponent {
  public userManagement: UserManagement | null = null;
  public userManagementSnapshot: UserManagement | null = null;
  private user: Profile | null = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: LocalizedMessageService, authService: AuthorizeService) {
    this.loadUsers();
    authService.getUser().subscribe(user => this.user = user);
  }

  public getUsers(): UserManagementItem[] {
    return this.userManagement ? this.userManagement.users : [];
  }

  public getRoles(): string[] {
    return this.userManagement ? this.userManagement.allRoles : [];
  }

  public userHasNoPermission(role: string): boolean {
    if (!this.userManagement) {
      return true;
    }

    const priority = this.userManagement.priorities[role] ?? Number.MAX_VALUE;

    return priority > this.getUserPriority(this.user, this.userManagement.priorities);
  }

  private getUserPriority(user: Profile | null, priorities: Record<string, number>): number {
    if (!user) {
      return -1;
    }

    return Math.max(...getRoles(user).map(role => priorities[role] ?? -1));
  }

  public isChanged(user: UserManagementItem): boolean {
    var snapshot = this.userManagementSnapshot?.users.find(u => u.id === user.id);

    return arrayEquals(user.roles, snapshot?.roles);
  }

  private loadUsers(): void {
    this.http.get<UserManagement>(this.baseUrl + 'usermanagement').subscribe(result => {
      this.userManagement = result;
      this.userManagementSnapshot = JSON.parse(JSON.stringify(result));
    }, error => console.error(error));
  }

  public updateUser(user: UserManagementItem): void {
    this.http.post<any>(this.baseUrl + 'usermanagement', user)
      .subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.USER_MANAGEMENT.SUCCESS_DETAIL' });

        const snapshot = this.userManagementSnapshot?.users.find(u => u.id === user.id);

        const index = this.userManagementSnapshot?.users.indexOf(snapshot!);

        this.userManagementSnapshot?.users.splice(index!, 1, JSON.parse(JSON.stringify(user)));
      }, (error: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.USER_MANAGEMENT.ERROR_DETAIL' }, extractFirstErrorMessage(error));
      });
  }
}

interface UserManagement {
  allRoles: string[]
  users: UserManagementItem[];
  priorities: Record<string, number>;
}

interface UserManagementItem {
  id: string;
  name: string;
  roles: string[];
}
