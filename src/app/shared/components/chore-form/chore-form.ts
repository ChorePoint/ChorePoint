import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AssignedKidToChore } from '../../../core/types/dtos/assigned-kid-to-chore';
import { Kid } from '../../../core/types/dtos/kid';
import { ChoreFrequency } from '../../../core/types/enums/chore-frequency';
import { EmojiPicker } from '../../../features/chores/components/emoji-picker/emoji-picker';
import { KidAssign } from '../../../features/chores/components/kid-assign/kid-assign';
import { DAYS_OF_WEEK } from '../../../features/chores/config/days-of-week';
import { DIFFICULTY_OPTIONS } from '../../../features/chores/config/difficulty-options';
import { FREQUENCY_OPTIONS } from '../../../features/chores/config/frequency-options';
import { ChoreFormGroup } from '../../types/chore-form-group';
import { LoadingEmoji } from '../loading-emoji/loading-emoji';

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

  @Output() submitted = new EventEmitter<void>();

  DaysOfWeek = DAYS_OF_WEEK;
  ChoreFrequencyOptions = FREQUENCY_OPTIONS;
  ChoreDifficultyOptions = DIFFICULTY_OPTIONS;

  choreFrequency = ChoreFrequency;

  selectedKids: Kid[] = [];

  submit(): void {
    this.submitted.emit();
  }

  adjustPoints(amount: number) {
    const current = this.form.get('points')?.value || 0;
    const next = Math.max(50, current + amount);
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
    this.router.navigate(['/dashboard/chores']);
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
}
