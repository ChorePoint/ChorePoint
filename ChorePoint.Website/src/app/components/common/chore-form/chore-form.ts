import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';
import { EmojiPicker } from '../../parent/chores/emoji-picker/emoji-picker';
import { KidAssign } from '../../parent/chores/kid-assign/kid-assign';
import { ChoreFormGroup } from '../../../core/types/interfaces/parent/chore-form-group';
import { Kid } from '../../../core/types/dtos/parent/kid';
import { DAYS_OF_WEEK } from '../../../core/consts/common/days-of-week';
import { FREQUENCY_OPTIONS } from '../../../core/consts/common/frequency-options';
import { DIFFICULTY_OPTIONS } from '../../../core/consts/common/difficulty-options';
import { ChoreFrequency } from '../../../core/types/enums/parent/chore-frequency';
import { AssignedKidToChore } from '../../../core/types/dtos/parent/assigned-kid-to-chore';

@Component({
  selector: 'app-chore-form',
  imports: [ReactiveFormsModule, LoadingEmoji, EmojiPicker, KidAssign],
  templateUrl: './chore-form.html',
  styleUrl: './chore-form.scss',
})
export class ChoreForm {
  router = inject(Router);

  @Input({ required: true }) form!: FormGroup<ChoreFormGroup>;
  @Input() loading = false;
  @Input() title = '➕ Add New Chore';
  @Input() submitText = 'Save Chore';
  @Input() kids!: Kid[];

  @Output() submitEmitter = new EventEmitter<void>();

  DaysOfWeek = DAYS_OF_WEEK;
  ChoreFrequencyOptions = FREQUENCY_OPTIONS;
  ChoreDifficultyOptions = DIFFICULTY_OPTIONS;

  choreFrequency = ChoreFrequency;

  selectedKids: Kid[] = [];

  submitted = false;

  submit(): void {
    this.submitted = true;
    this.submitEmitter.emit();
  }

  adjustPoints(amount: number) {
    const current = this.form.get('points')?.value || 0;
    const next = Math.max(10, current + amount);
    this.form.patchValue({ points: next });
  }

  selectKid(kidId: number) {
    this.selectedKids = this.kids.filter((kid) => kid.kidId == kidId || kidId === -1);

    this.form.patchValue({
      assignedKids: this.selectedKids.map((selectedKid) => ({
        kidId: selectedKid.kidId,
        isVisible: true,
        dayOfWeek: 0,
      })),
    });
  }

  back() {
    this.router.navigate(['/parent/chores']);
  }

  getKidSetting(kidId: number, control: keyof AssignedKidToChore) {
    return (this.form.get('assignedKids')?.value as AssignedKidToChore[]).filter(
      (kid) => kid.kidId == kidId,
    )[0][control];
  }

  setKidSetting<K extends keyof AssignedKidToChore>(
    kidId: number,
    control: K,
    value: AssignedKidToChore[K],
  ) {
    const currentAssignedKids = this.form.get('assignedKids')!.value;

    const newSetting = currentAssignedKids!.filter((kid) => kid.kidId == kidId)[0];
    newSetting[control] = value;
  }

  fieldHasError(fieldName: string) {
    return this.submitted && this.form.get(fieldName)?.invalid;
  }
}
