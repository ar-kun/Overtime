const schedule = document.querySelector('#schedule');
const date = document.querySelector('#date');
const daysContainer = document.querySelector('#days');
const prev = document.querySelector('#prev');
const next = document.querySelector('#next');
const todayBtn = document.querySelector('#today-btn');
const gotoBtn = document.querySelector('#goto-btn');
const dateInput = document.querySelector('#date-input');

let today = new Date();
let activeDate;
let month = today.getMonth();
let year = today.getFullYear();

const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'Octobor', 'November', 'Desember'];

//  add to days
const initCalender = () => {
    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);
    const prevLastDay = new Date(year, month, 0);
    const prevDays = prevLastDay.getDate();
    const lastDate = lastDay.getDate();
    const day = firstDay.getDay();
    const nextDays = 7 - lastDay.getDay() - 1;

    date.innerHTML = `${months[month]} ${year}`;

    let days = '';
    const randomId = Math.random().toString(36).substring(2, 10);

    //prev month days
    for (let x = day; x > 0; x--) {
        days += `<div class="prev-date" >${prevDays - x + 1}</div>`;
    }

    //Curret month days
    for (let i = 1; i <= lastDate; i++) {
        if (i === new Date().getDate() && month === new Date().getMonth() && year === new Date().getFullYear()) {
            days += `<div class="today" >${i}</div>`;
        } else {
            const isWeekday = new Date(year, month, i).getDay() === 0 || new Date(year, month, i).getDay() === 6 ? 1 : 0;

            days += `
      <div>
        <div class="hover:bg-secondary/75 hover:text-primary rounded-lg py-5 px-[3.75rem]" id="${i}-${randomId}" data-modal-target="default-${i}-${randomId}" data-modal-toggle="default-${i}-${randomId}">${i}</div>

      <!-- modal -->
    <div
     id="default-${i}-${randomId}"
     tabindex="-1"
     aria-hidden="true"
     class="hidden absolute bg-secondary/50 w-full h-full top-0 left-0 flex justify-center items-center ease-out transition-all duration-300 backdrop-blur-md text-primary rounded-xl"
    >
     <div class="bg-third w-1/2 rounded-lg p-5 relative shadow-md shadow-slate-500">
      <div class="flex px-5 py-2 gap-5" id="today-date">
       <div class="" id="event-day">Wed</div>
       <div class="" id="event-date">${i}th ${months[month]} ${year}</div>
      </div>
      <div class="absolute right-0 top-0 text-3xl p-5">
       <i class="fas fa-times" data-modal-close="default-${i}-${randomId}"></i>
      </div>
      <div class="" id="events"></div>
      <div class="flex items-center" id="add-event-wrapper">
       <form class="w-full">
        <div class="relative z-0 w-full mb-6 group">
         <input
          type="date"
          name="floating_date"
          id="floating_date"
          class="hidden py-2.5 px-0 w-full text-sm text-primary bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          value="${year}-${month + 1}-${i < 10 ? '0' + i : i}"
          required
          readonly
         />
        </div>
        <div class="relative z-0 w-full mb-6 group">
         <input
          type="number"
          name="floating_duration"
          id="floating_duration"
          class="block py-2.5 px-0 w-full text-sm text-primary bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          required
         />
         <label
          for="floating_duration"
          class="peer-focus:font-medium absolute text-sm text-primary dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
          >Duration</label
         >
        </div>
        
        <div class="relative z-0 w-full mb-6 group">
         <input
          type="text"
          name="floating_remarks"
          id="floating_remarks"
          class="block py-2.5 px-0 w-full text-sm text-primary bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          required
         />
         <label
          for="floating_remarks"
          class="peer-focus:font-medium absolute text-sm text-primary dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
          >Remarks</label
         >
        </div>
        <div class="relative z-0 w-full mb-6 group">
         <input
          type="number"
          name="floating_type"
          id="floating_type"
          class="hidden py-2.5 px-0 w-full text-sm text-primary bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          value="${isWeekday}"
          required
          readonly
         />
        </div>

        <div
         class="group bg-primary rounded-xl text-center hover:bg-secondary focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium text-sm w-full sm:w-auto px-5 py-2.5 dark:bg-third dark:hover:bg-bg-secondary dark:focus:ring-secondary shadow-md shadow-slate-500"
        >
         <button type="submit" class="text-third text-lg group-hover:text-primary">Send Request Overtime</button>
        </div>
       </form>
      </div>
     </div>
    </div>
      </div>
      `;
        }
    }

    //next month days

    for (let j = 1; j <= nextDays; j++) {
        days += `<div class="next-date" >${j}</div>`;
    }

    daysContainer.innerHTML = days;
};

initCalender();

//  prev month
const prevMonth = () => {
    month--;
    if (month < 0) {
        month = 11;
        year--;
    }
    initCalender();
};

//  next month
const nextMonth = () => {
    month++;
    if (month > 11) {
        month = 0;
        year++;
    }
    initCalender();
};

//event next and prev
prev.addEventListener('click', prevMonth);
next.addEventListener('click', nextMonth);

//Our calender is ready
todayBtn.addEventListener('click', () => {
    today = new Date();
    month = today.getMonth();
    year = today.getFullYear();
    initCalender();
});

//Go to date
dateInput.addEventListener('input', (e) => {
    dateInput.value = dateInput.value.replace(/[^0-9/]/g, '');
    if (dateInput.value.length === 2) {
        dateInput.value += '/';
    }
    if (dateInput.value.length > 7) {
        dateInput.value = dateInput.value.slice(0, 7);
    }
    if (e.inputType === 'deleteContentBackward') {
        if (dateInput.value.length === 3) {
            dateInput.value = dateInput.value.slice(0, 2);
        }
    }
});

const gotoDate = () => {
    const dateArr = dateInput.value.split('/');
    if (dateArr.length === 2) {
        if (dateArr[0] > 0 && dateArr[0] < 13 && dateArr[1].length === 4) {
            month = dateArr[0] - 1;
            year = dateArr[1];
            initCalender();
            return;
        }
    }
    alert('Invalid Date');
};
gotoBtn.addEventListener('click', gotoDate);

const modalTriggers = document.querySelectorAll('[data-modal-toggle]');
const modalCloseTriggers = document.querySelectorAll('[data-modal-close]');

modalTriggers.forEach((trigger) => {
    trigger.addEventListener('click', () => {
        const targetId = trigger.getAttribute('data-modal-target');
        const targetModal = document.getElementById(targetId);
        if (targetModal) {
            targetModal.classList.remove('hidden');
        }
    });
});

modalCloseTriggers.forEach((closeTrigger) => {
    closeTrigger.addEventListener('click', () => {
        const targetId = closeTrigger.getAttribute('data-modal-close');
        const targetModal = document.getElementById(targetId);
        if (targetModal) {
            targetModal.classList.add('hidden');
        }
    });
});