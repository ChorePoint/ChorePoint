import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Kid } from '../../../core/types/dtos/kid';
import { ChoreFrequency } from '../../../core/types/enums/chore-frequency';
import { EmojiPicker } from '../../../features/chores/components/emoji-picker/emoji-picker';
import { KidAssign } from '../../../features/chores/components/kid-assign/kid-assign';
import { DAYS_OF_WEEK } from '../../../features/chores/config/days-of-week';
import { DIFFICULTY_OPTIONS } from '../../../features/chores/config/difficulty-options';
import { FREQUENCY_OPTIONS } from '../../../features/chores/config/frequency-options';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';

@Component({
  selector: 'app-chore-form',
  imports: [ReactiveFormsModule, LoadingEmoji, EmojiPicker, KidAssign],
  templateUrl: './chore-form.html',
  styleUrl: './chore-form.scss',
})
export class ChoreForm {
  router = inject(Router);

  @Input({ required: true }) form!: FormGroup;
  @Input() loading = false;
  @Input() title = '➕ Add New Chore';
  @Input() submitText = 'Save Chore';
  @Input() kids!: Kid[];

  @Output() submitted = new EventEmitter<void>();

  DaysOfWeek = DAYS_OF_WEEK;
  ChoreFrequencyOptions = FREQUENCY_OPTIONS;
  ChoreDifficultyOptions = DIFFICULTY_OPTIONS;

  choreFrequency = ChoreFrequency;

  submit(): void {
    this.submitted.emit();
  }

  adjustPoints(amount: number) {
    const current = this.form.get('points')?.value || 0;
    const next = Math.max(50, current + amount);
    this.form.patchValue({ points: next });
  }

  back() {
    this.router.navigate(['/dashboard/chores']);
  }
}
