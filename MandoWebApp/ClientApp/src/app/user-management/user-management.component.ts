
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { arrayEquals } from '../utilities/array-util';
import { extractFirstErrorMessage } from '../utilities/error-util';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  providers: [MessageService]
})
export class UserManagementComponent {
  public userManagement: UserManagement | null = null;
  public userManagementSnapshot: UserManagement | null = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private messageService: MessageService) {
    this.loadUsers();
  }

  public getUsers(): UserManagementItem[] {
    return this.userManagement ? this.userManagement.users : [];
  }

  public getRoles(): string[] {
    return this.userManagement ? this.userManagement.allRoles : [];
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
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'User updated!' });

        const snapshot = this.userManagementSnapshot?.users.find(u => u.id === user.id);

        const index = this.userManagementSnapshot?.users.indexOf(snapshot!);

        this.userManagementSnapshot?.users.splice(index!, 1, JSON.parse(JSON.stringify(user)));
      }, (error: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'We failed to update the user. Details: ' + extractFirstErrorMessage(error) });
      });
  }
}

interface UserManagement {
  allRoles: string[]
  users: UserManagementItem[];
}

interface UserManagementItem {
  id: string;
  name: string;
  roles: string[];
}
