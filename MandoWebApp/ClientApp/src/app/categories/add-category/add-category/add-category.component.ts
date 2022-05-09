import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/_services/category.service';
import { LocalizedMessageService } from 'src/app/_services/localized-message.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent { 

  public newCategoryEnName: string | undefined;
  public newCategoryHuName: string | undefined;

  constructor(
    @Inject('BASE_URL') public baseUrl: string,
    private messageService: LocalizedMessageService,
    private categoryService: CategoryService) {};

    createCategory() {
      this.categoryService.addCategory({
        ENName: this.capitalizeFirstLetter(this.newCategoryEnName!),
        HUName: this.capitalizeFirstLetter(this.newCategoryHuName!),
      }).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'MESSAGE.SUCCESS', detail: 'MESSAGE.CATEGORIES.SUCCESS_DETAIL' });
          this.resetFields();
        },
        error: (error: HttpErrorResponse) => {
          this.messageService.add({ severity: 'error', summary: 'MESSAGE.ERROR', detail: 'MESSAGE.CATEGORIES.ERROR_DETAIL' });
        }
      })
    }
  
    resetFields() {
      this.newCategoryEnName = undefined
      this.newCategoryHuName = undefined
    }

    capitalizeFirstLetter(text: string) : string {
      return text.charAt(0).toUpperCase() + text.slice(1);
    }
}
