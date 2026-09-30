import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { Kid } from '../../../../core/types/dtos/kid';
import { ModalOverlay } from '../../layout/modal-overlay/modal-overlay';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-login-code-generation',
  imports: [ModalOverlay],
  templateUrl: './login-code-generation.html',
  styleUrl: './login-code-generation.scss',
})
export class LoginCodeGeneration {
  private authService = inject(AuthService);

  @Input() kids!: Kid[];

  selectedKidId = signal<number | null>(null);
  generatedCode = signal<string[] | null>(null);
  isGeneratingCode = signal(false);
  codeExpiryMinutes = signal(10);

  @Output() changeLoginModalVisibility = new EventEmitter<boolean>();

  closeLoginCodeModal() {
    this.changeLoginModalVisibility.emit(false);
    this.selectedKidId.set(null);
    this.generatedCode.set(null);
  }
  selectKidForCode(id: number) {
    this.selectedKidId.set(id);
  }

  generateLoginCode() {
    const kidId = this.selectedKidId();
    if (kidId !== null) {
      this.authService.getLoginCode(kidId).subscribe({
        next: (codeResponse) => {
          if (codeResponse?.loginCode !== null)
            this.generatedCode.set(codeResponse!.loginCode.split(''));
        },
      });
    }

    this.isGeneratingCode.set(false);
  }
  regenerateCode() {
    this.generatedCode.set(null);
    this.generateLoginCode();
  }
}
