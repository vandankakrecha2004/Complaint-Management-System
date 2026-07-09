import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Complaint } from '../../services/complaint';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ViewChild, ElementRef } from '@angular/core';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

declare var bootstrap: any;

declare var bootstrap: any;

@Component({
  standalone: true,
  selector: 'app-complaint-list',
  templateUrl: './complaint-list.html',
  styleUrl: './complaint-list.css',
  imports: [CommonModule, FormsModule],
})
export class ComplaintListComponent implements OnInit {
  @ViewChild('successToast') toastElement!: ElementRef;

  showSuccessToast() {
    const toast = new bootstrap.Toast(this.toastElement.nativeElement, {
      delay: 3000, // 3 seconds
    });

    toast.show();
  }
  complaints: any[] = [];
  allComplaints: any[] = [];
  selectedStatusId: number | null = null;

  // for dropdown
  subjects: any[] = [];
  systemTypes: any[] = [];

  searchText: string = '';

  // for pagination---------
  currentPage: number = 1;
  itemsPerPage: number = 5;

  get paginatedComplaints() {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.complaints.slice(start, end);
  }

  get totalPages(): number {
    return Math.ceil(this.complaints.length / this.itemsPerPage);
  }
  // pagination over------------

  companies: any[] = [
    { id: 500021, name: 'HKRP' },
    { id: 800541, name: 'Nova Trice' },
    { id: 600057, name: 'GUVNL' },
  ];

  getCompanyName(id: number): string {
    const company = this.companies.find((c) => c.id === id);
    return company ? company.name : '-';
  }

  users: string[] = ['Arjun_yadav'];

  // files
  resolveFile: File | null = null;
  closeFile: File | null = null;

  newComplaint: any = {
    deviceId: '1485',
    farmerId: 1,
    applicationId: 1,
    compSubjectId: null,
    systemTypeId: null,
    allocSubNockId: -1,
    districtId: 1,
    compGeneratedDate: null,
    compStatusId: 1,
    compDetails: 'Unit Stat on 18-9-21,but installation Pending.',
    closeRemarks: '',
    resolveRemarks: '',
  };

  constructor(
    private complaintService: Complaint,
    private cdr: ChangeDetectorRef,
    public auth: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loadSystemTypes();
    this.loadComplaints();
    if (this.auth.isLoggedIn()) {
      this.router.navigate(['/complaints']);
    }
  }

  onResolveFileSelected(event: any) {
    const file = event.target.files[0];
    if (!file) return;

    const allowedExtensions = ['png', 'jpg', 'jpeg', 'pdf'];
    const fileExtension = file.name.split('.').pop()?.toLowerCase();

    // ✅ Extension Validation
    if (!fileExtension || !allowedExtensions.includes(fileExtension)) {
      alert('Only JPEG, PNG, JPG and PDF files are allowed');
      event.target.value = '';
      this.resolveFile = null;
      return;
    }

    const maxSize = 5 * 1024 * 1024; // 5 MB

    // ✅ Size Validation
    if (file.size > maxSize) {
      alert('File size cannot be more than 5 MB');
      event.target.value = '';
      this.resolveFile = null;
      return;
    }

    this.resolveFile = file;
  }

  onCloseFileSelected(event: any) {
    const file = event.target.files[0];
    if (!file) return;

    const allowedExtensions = ['png', 'jpg', 'jpeg', 'pdf'];
    const fileExtension = file.name.split('.').pop()?.toLowerCase();

    // ✅ Extension Validation
    if (!fileExtension || !allowedExtensions.includes(fileExtension)) {
      alert('Only JPEG, PNG, JPG and PDF files are allowed');
      event.target.value = '';
      this.closeFile = null;
      return;
    }

    const maxSize = 5 * 1024 * 1024; // 5 MB

    // ✅ Size Validation
    if (file.size > maxSize) {
      alert('File size cannot be more than 5 MB');
      event.target.value = '';
      this.closeFile = null;
      return;
    }

    this.closeFile = file;
  }

  loadSubjects(systemTypeId?: number) {
    this.complaintService.getSubjects(systemTypeId).subscribe({
      next: (res) => {
        this.subjects = res;
        this.cdr.detectChanges();
      },
      error: (err) => console.error(err),
    });
  }

  loadSystemTypes() {
    this.complaintService.getSystemTypes().subscribe({
      next: (res) => {
        this.systemTypes = res;
        // log all subjects
        // console.log(this.systemTypes);
      },
      error: (err) => console.error(err),
    });
  }

  onSystemChange(systemTypeId: any) {
    if (!systemTypeId) {
      this.subjects = [];
      this.newComplaint.compSubjectId = null;
      return;
    }

    this.loadSubjects(Number(systemTypeId));
  }

  loadComplaints() {
    this.complaintService.getAllComplaints().subscribe({
      next: (data: any) => {
        this.allComplaints = data;
        this.complaints = data;
        // show all data
        // console.log(data);
        // this.currentPage = 1;

        this.cdr.detectChanges();
      },
      error: (err: any) => console.error(err),
    });
  }

  openAddModal() {
    const modal = new bootstrap.Modal(document.getElementById('addComplaintModal'));
    modal.show();
  }

  closeModal() {
    const modalEl = document.getElementById('addComplaintModal');
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal?.hide();

    this.resetModal();
  }

  saveComplaint(form: any) {
    if (
      !this.newComplaint.farmerId ||
      !this.newComplaint.applicationId ||
      !this.newComplaint.compSubjectId ||
      !this.newComplaint.systemTypeId ||
      !this.newComplaint.allocSubNockId ||
      !this.newComplaint.districtId ||
      !this.newComplaint.compGeneratedDate ||
      !this.newComplaint.compStatusId
    ) {
      console.log('Enter required Field');
      return;
    }

    this.complaintService.addComplaint(this.newComplaint).subscribe({
      next: (res) => {
        // show saved info
        // console.log('Saved', res);
        this.loadComplaints(); // refresh table
        this.closeModal();

        this.resetModal();
        this.showSuccessToast(); // 🔥 show alert
        this.cdr.detectChanges();
      },
      error: (err) => console.error(err),
    });
  }

  resetModal() {
    // ✅ Reset model
    this.newComplaint = {
      compDetails: 'Unit Stat on 18-9-21,but installation Pending.',
      deviceId: '1485',
      farmerId: 1,
      applicationId: 1,
      compSubjectId: null,
      systemTypeId: null,
      allocSubNockId: -1,
      districtId: 1,
      compGeneratedDate: null,
      compStatusId: 1,
    };
  }

  // ACKNOWLEDGE
  acknowledge(c: any, event: any) {
    if (c.compStatusId >= 2) return;

    const ok = confirm('Are you sure you want to acknowledge this complaint?');

    if (!ok) {
      event.target.checked = false;
      return;
    }
    this.changeStatus(c, 2);
  }

  // GEBERATE TICKET
  generateTicket(c: any) {
    alert(`Ticket Generated : ${(Math.random() * 10000000000 + 1).toFixed(0)}`);
    this.changeStatus(c, 3);
  }

  // ALLOCATE
  allocate(c: any) {
    if (!c.selectedCompany) return;

    // 🔹 1. Status update payload (your existing)
    const payload = {
      complaintId: c.id,
      statusId: 5,
      allocDeptId: c.selectedCompany,
    };

    // 🔹 2. Allocation table payload (NEW)
    const allocationPayload = {
      complaintId: c.id,
      complainReason: c.compDetails ?? '',
      allocatedAgency: c.selectedCompany,
      allocatedSubNock: -1,
      createdBy: '6f26d7fab77a4c1b8957215b65c9dfed',
    };

    // 🔹 3. Call ALLOCATION API (NEW)
    this.complaintService.allocateComplaint(allocationPayload).subscribe();

    // 🔹 4. Your existing STATUS update (unchanged)
    this.complaintService.changeComplaintStatus(payload).subscribe({
      next: () => {
        c.compStatusId = 5;
        c.allocDeptId = c.selectedCompany;
        this.loadComplaints();
        this.cdr.detectChanges();
      },
      error: (err: any) => console.error(err),
    });
  }

  assign(c: any) {
    if (!c.selectedUser) return;

    // 🔹 1. Your existing status payload (unchanged)
    const payload = {
      complaintId: c.id,
      statusId: 4,
      assignedUser: c.selectedUser,
    };

    // 🔹 2. NEW → comp_assignment payload
    const assignmentPayload = {
      complaintId: c.id,
      assignmentTo: c.selectedUser,
      analysis: c.analysis ?? 'Default Assign',
      description: c.description ?? 'Default Assign',
      createdBy: '6f26d7fab77a4c1b8957215b65c9dfed',
    };

    // 🔹 3. CALL new API → insert into comp_assignment
    this.complaintService.assignComplaint(assignmentPayload).subscribe({
      error: (err: any) => console.error(err),
    });

    // 🔹 4. Your existing status update (unchanged)
    this.complaintService.changeComplaintStatus(payload).subscribe({
      next: () => {
        c.compStatusId = 4;
        c.assignedUser = c.selectedUser;
        this.loadComplaints();
        this.cdr.detectChanges();
      },
    });
  }

  // RESOLVE
  resolve(c: any) {
    if (!this.resolveFile) {
      alert('Select resolve file');
      return;
    }

    if (!c.resolveRemarks) {
      alert('Enter resolve remarks');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.resolveFile);
    formData.append('complaintId', c.id);
    formData.append('type', 'resolve');

    console.log(formData);

    this.complaintService.uploadComplaintFile(formData).subscribe({
      next: () => {
        const payload = {
          complaintId: c.id,
          statusId: 6,
          resolveRemarks: c.resolveRemarks,
        };

        this.complaintService.changeComplaintStatus(payload).subscribe({
          next: () => {
            c.resolveRemarks = '';
            this.resolveFile = null;
            this.loadComplaints();
          },
        });
        alert('Resolve File Uploaded Successfully');
      },
      error: (msg) => {
        console.log(msg);
      },
    });
  }

  // CLOSE
  close(c: any) {
    if (!this.closeFile) {
      alert('Select close file');
      return;
    }

    if (!c.closeRemarks) {
      alert('Enter closing remarks');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.closeFile);
    formData.append('complaintId', c.id);
    formData.append('type', 'close');

    this.complaintService.uploadComplaintFile(formData).subscribe({
      next: () => {
        const payload = {
          complaintId: c.id,
          statusId: 7,
          closeRemarks: c.closeRemarks,
        };

        this.complaintService.changeComplaintStatus(payload).subscribe({
          next: () => {
            c.closeRemarks = '';
            this.closeFile = null;
            this.loadComplaints();
          },
        });
        alert('Resolve File Uploaded Successfully');
      },
    });
  }

  changeStatus(c: any, statusId: number) {
    const payload = {
      complaintId: c.id,
      statusId: statusId,
    };

    this.complaintService.changeComplaintStatus(payload).subscribe({
      next: () => {
        // ✅ Update numeric status
        c.compStatusId = statusId;
        this.loadComplaints();
        this.cdr.detectChanges();
      },
      error: (err: any) => console.error(err),
    });
  }

  logout() {
    this.auth.logout();
    location.reload();
  }

  downloadFile(complaintId: number, type: string) {
    this.complaintService.downloadComplaintFile(complaintId, type).subscribe({
      next: (blob: Blob) => {
        // If backend somehow returns empty file
        if (!blob || blob.size === 0) {
          alert('File not found');
          return;
        }

        const fileURL = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = fileURL;
        a.download = `${type}-file-${complaintId}`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        window.URL.revokeObjectURL(fileURL);
      },

      error: (err) => {
        if (err.status === 404) {
          alert('File not found');
        } else if (err.status === 400) {
          alert('Invalid request');
        } else {
          alert('Something went wrong while downloading file');
        }
      },
    });
  }

  statusHistory: any[] = [];
  selectedComplaintId: number | null = null;

  viewDetails(c: any) {
    this.selectedComplaintId = c.id;

    // ✅ Clear previous data immediately
    this.statusHistory = [];

    this.complaintService.getStatusHistory(c.id).subscribe({
      next: (res) => {
        this.statusHistory = res;

        const modal = new bootstrap.Modal(document.getElementById('statusHistoryModal'));
        modal.show();
        this.cdr.detectChanges();
      },
      error: () => {
        alert('No status history found');
      },
    });
  }

  // ================= STATUS COUNTS =================

  get totalGenerated() {
    return this.allComplaints.filter((c) => c.compStatusId === 1).length;
  }

  get totalAcknowledged() {
    return this.allComplaints.filter((c) => c.compStatusId === 2).length;
  }

  get totalTicketGenerated() {
    return this.allComplaints.filter((c) => c.compStatusId === 3).length;
  }

  get totalAllocated() {
    return this.allComplaints.filter((c) => c.compStatusId === 5).length;
  }

  get totalAssigned() {
    return this.allComplaints.filter((c) => c.compStatusId === 4).length;
  }

  get totalResolved() {
    return this.allComplaints.filter((c) => c.compStatusId === 6).length;
  }

  get totalClosed() {
    return this.allComplaints.filter((c) => c.compStatusId === 7).length;
  }

  filterByStatus(statusId: number | null) {
    this.selectedStatusId = statusId;
    this.currentPage = 1; // reset pagination

    this.applyFilters();
  }

  applyFilters() {
    let data = this.allComplaints;

    // 🔹 Status Filter
    if (this.selectedStatusId) {
      data = data.filter((c) => c.compStatusId === this.selectedStatusId);
    }

    // 🔹 Search Filter
    if (this.searchText && this.searchText.trim() !== '') {
      const text = this.searchText.toLowerCase();

      data = data.filter(
        (c) =>
          c.complaintUId?.toLowerCase().includes(text) ||
          c.farmerId?.toString().includes(text) ||
          c.subject?.toLowerCase().includes(text) ||
          c.systemType?.toLowerCase().includes(text) ||
          c.compDetails?.toLowerCase().includes(text) ||
          c.assignedUser?.toLowerCase().includes(text),
      );
    }

    this.complaints = data;
  }

  exportToPDF() {
    if (!this.complaints || this.complaints.length === 0) {
      alert('No data to export');
      return;
    }

    const doc = new jsPDF('landscape'); // landscape for wide table

    const tableData = this.complaints.map((c) => [
      c.id,
      c.complaintUId,
      c.farmerId,
      c.districtId,
      c.subject || '-',
      c.systemType,
      c.compDetails,
      c.complaintStatus,
      this.getCompanyName(c.allocDeptId),
      c.assignedUser || '-',
      c.compGeneratedDate,
      c.createdOn,
    ]);

    autoTable(doc, {
      head: [
        [
          'ID',
          'Complaint UID',
          'Farmer ID',
          'District ID',
          'Subject',
          'System Type',
          'Details',
          'Status',
          'Allocated Dept',
          'Assigned User',
          'Generated Date',
          'Created On',
        ],
      ],
      body: tableData,
      styles: {
        fontSize: 8,
      },
      headStyles: {
        fillColor: [33, 37, 41], // dark header
      },
    });

    doc.save(`Complaint_List_${new Date().getTime()}.pdf`);
  }
}
